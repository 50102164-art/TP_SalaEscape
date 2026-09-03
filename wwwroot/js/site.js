// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function verificarCondiciones(){
    const respuesta = document.getElementById("respuesta")?.value.trim();

    if(respuesta === "clave_correcta"){
        return true;
    } 
    else {
        if(respuesta === "-")
            alert("Por favor, ingrese una respuesta.");
        else
            alert("Respuesta incorrecta. Inténtalo de nuevo.");

        return false;
    }
}

function cambiarFondo() {
    let img = document.getElementById("RecursoUrl");
    document.body.style.backgroundImage = `url('${img.src}')`;
}

