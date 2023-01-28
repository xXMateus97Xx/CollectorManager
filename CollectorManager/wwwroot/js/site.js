// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(() => {
	$(".btn-delete").click((e) => {
		let el = $(e.currentTarget);
		let name = el.data('name');
		if (!confirm(`Deseja realmente excluir o registro ${name}`))
			return;

		$.ajax({
			url: el.data('route'),
			type: 'POST',
			success: (data) => {
				location.reload();
			},
			error: () => {
				alert("Erro ao deletar registro");
			}
		});
	});

	$(".js-select2").select2({
		closeOnSelect: false,
		placeholder: "Clique para selecionar uma opção",
		allowHtml: true,
		allowClear: true,
		tags: true
	});

	$('.icons_select2').select2({
		width: "100%",
		templateSelection: iformat,
		templateResult: iformat,
		allowHtml: true,
		placeholder: "Clique para selecionar uma opção",
		dropdownParent: $('.select-icon'),
		allowClear: true,
		multiple: false
	});


	function iformat(icon, badge,) {
		var originalOption = icon.element;
		var originalOptionBadge = $(originalOption).data('badge');

		return $('<span><i class="fa ' + $(originalOption).data('icon') + '"></i> ' + icon.text + '<span class="badge">' + originalOptionBadge + '</span></span>');
	}
});
