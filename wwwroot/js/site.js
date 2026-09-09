// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function verificarCondiciones(){
    const respuesta = document.getElementById("respuesta")?.value.trim();
    const nivel = document.getElementById("nivel")?.value.trim();

    if(nivel === "0" && respuesta != "" || respuesta === "clave_correcta"){
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

let pistasMostradas = {
    1: false,
    2: false,
    3: false
};

function mostrarPista(numeroPista) {
    const contenido = document.getElementById(`contenidoPista${numeroPista}`);
    const btn = document.getElementById(`btnPista${numeroPista}`);
    
    // Mostrar u ocultar la pista
    if (contenido.style.display === 'none') {
        contenido.style.display = 'block';
        pistasMostradas[numeroPista] = true;
        btn.textContent = `🔍 Pista ${numeroPista} (Mostrada)`;
        btn.style.backgroundColor = '#FFD700';
        btn.style.color = 'black';
        
        // Habilitar siguiente pista si existe
        if (numeroPista < 3) {
            const btnSiguiente = document.getElementById(`btnPista${numeroPista + 1}`);
            btnSiguiente.disabled = false;
            btnSiguiente.style.backgroundColor = '#FFD700';
            btnSiguiente.style.color = 'black';
            btnSiguiente.style.cursor = 'pointer';
        }
    } else {
        contenido.style.display = 'none';
        pistasMostradas[numeroPista] = false;
        btn.textContent = `🔍 Pista ${numeroPista}`;
        btn.style.backgroundColor = '#FFD700';
        btn.style.color = 'black';
    }
}

// El formulario se envía al servidor (/Sala/Respuesta) que valida y redirige.
