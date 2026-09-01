// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function verificarCondiciones(){
    if(document.getElementById("respuesta").value === "clave_correcta"){
        return true;
    } 
    else {
        if(document.getElementById("respuesta").value === "-")
            alert("Por favor, ingrese una respuesta.");
        else
            alert("Respuesta incorrecta. Inténtalo de nuevo.");
        return false;
    }
}