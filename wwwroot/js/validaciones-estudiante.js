$(document).ready(function () {
    // Validación para la página de Crear
    $("#Matricula").on("blur", function () {
        var matricula = $(this).val();
        var url = '/Estudiantes/VerificarMatricula'; // Necesitaremos crear esta acción
        var errorSpan = $("#errorMatricula");

        if (matricula) {
            $.getJSON(url, { matricula: matricula }, function (existe) {
                if (existe) {
                    errorSpan.text("Esta matrícula ya está en uso.");
                    $("#btnGuardar").prop("disabled", true); // Deshabilita el botón de guardar
                } else {
                    errorSpan.text("");
                    $("#btnGuardar").prop("disabled", false);
                }
            });
        } else {
            errorSpan.text("");
            $("#btnGuardar").prop("disabled", false);
        }
    });
});