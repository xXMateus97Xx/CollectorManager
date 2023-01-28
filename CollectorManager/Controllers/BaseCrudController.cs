using CollectorManager.Data;
using CollectorManager.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace CollectorManager.Controllers;

public abstract class BaseCrudController<TService, TEntity, TResumeModel, TDetailsModel, TSearchRequest, TDetailsSearchRequest> : BaseController
    where TService : IBaseCrudService<TEntity, TResumeModel, TDetailsModel, TSearchRequest, TDetailsSearchRequest>
    where TEntity : BaseEntity
    where TResumeModel : BaseEntityModel
    where TDetailsModel : UpdateEntityRequest<TEntity>
    where TSearchRequest : PageableSearchRequest<TEntity>
    where TDetailsSearchRequest : SearchOneRequest<TEntity>, new()
{
    private readonly TService _service;

    public BaseCrudController(TService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index([FromQuery] TSearchRequest request, CancellationToken cancellationToken)
    {
        var response = await _service.GetAllAsync(request, cancellationToken);
        if (!response.IsSuccess)
            return RedirectToAction("Index", "Home");

        await LoadFilterDataAsync(cancellationToken);

        ViewBag.Search = request;
        ViewBag.SearchRouteValues = request.ToRouteValues();

        return View(response.Data);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = PrepareCreateModel();
        if (model is null)
            return RedirectToAction("Index", "Home");

        await LoadDataAsync(cancellationToken);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TDetailsModel model, CancellationToken cancellationToken)
    {
        var response = await _service.InsertAsync(model, cancellationToken);
        if (!response.IsSuccess)
        {
            (await model.ValidateAsync(cancellationToken)).AddToModelState(ModelState);

            await LoadDataAsync(cancellationToken);
            return View(model);
        }

        return RedirectToAction("Index", GetIndexRouteValues(model));
    }

    public async Task<IActionResult> Edit(int id, [FromQuery] TDetailsSearchRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;

        var response = await _service.GetByIdAsync(request, cancellationToken);
        if (!response.IsSuccess)
            return RedirectToAction("Index");

        await LoadDataAsync(cancellationToken);

        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TDetailsModel model, CancellationToken cancellationToken)
    {
        var response = await _service.UpdateAsync(model, cancellationToken);
        if (!response.IsSuccess)
        {
            (await model.ValidateAsync(cancellationToken)).AddToModelState(ModelState);

            await LoadDataAsync(cancellationToken);
            return View(model);
        }

        return RedirectToAction("Index", GetIndexRouteValues(model));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, [FromQuery] TDetailsSearchRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;

        var response = await _service.DeleteAsync(request, cancellationToken);

        return RedirectToAction("Index");
    }

    protected virtual object? GetIndexRouteValues(TDetailsModel model) => null;

    protected abstract TDetailsModel? PrepareCreateModel();

    protected virtual Task LoadFilterDataAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    protected virtual Task LoadDataAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
