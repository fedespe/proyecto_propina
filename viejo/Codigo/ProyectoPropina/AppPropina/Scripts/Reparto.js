var reparto;
var repartoCompleto;

//Al iniciar
$(function () {
    ocultarRepartoCompleto();
    obtenerReparto();
});

function mostrarRepartoCompleto() {
    $("#divReparto").hide();
    $("#divRepartoCompleto").show();
}
function ocultarRepartoCompleto() {
    $("#divReparto").show();
    $("#divRepartoCompleto").hide();
}



function cargarDatos() {
    cargarDatosPrincipales()
    cargarAusencias();
    mostrarRepartoCompleto();    
    enviarReparto();
}
function cargarDatosPrincipales() {
    reparto["FechaReparto"] = document.getElementById("FechaReparto").value;
    reparto["MontoPesos"] = document.getElementById("MontoPesos").value;
    reparto["MontoDolares"] = document.getElementById("MontoDolares").value;
    console.log(reparto);
}

function cargarAusencias(input) {
    //var texto = "";
    for (var i in reparto["Empleados"]) {
        //console.log(clientesObtenidos[i]);
        var j = "txtAusencias" + reparto["Empleados"][i]["Id"];
        console.log(document.getElementById(j).value);
        //texto = texto + clientesObtenidos[i]["Id"] + ";" + document.getElementById(j).value + " ";
        reparto["Empleados"][i]["RepartoEmpleado"]["Ausencias"] = document.getElementById(j).value;
    }
    //document.getElementById("TxtFaltasTotales").value = texto;
    //console.log(document.getElementById("TxtFaltasTotales").value);
    console.log(reparto);
}

function completarRepartoCompleto() {
    //REPARTO
    console.log("completarReparto");
    console.log(repartoCompleto);
    //var fechaRepartoCompleto = new Date(repartoCompleto["FechaReparto"].substring(6,repartoCompleto["FechaReparto"].length-2));
    //console.log(fechaRepartoCompleto);
    $("#FechaRepartoCompleto").html(reparto["FechaReparto"] + " ARREGLAR");
    $("#MontoPesosCompleto").html(repartoCompleto["MontoPesos"]);
    $("#MontoDolaresCompleto").html(repartoCompleto["MontoDolares"]);
    $("#FondoPesosCompleto").html(repartoCompleto["FondoPesos"]);
    $("#FondoDolaresCompleto").html(repartoCompleto["FondoDolares"]);

    //EMPLEADOS
    for (var i in repartoCompleto["Empleados"]) {
        var empleado = repartoCompleto["Empleados"][i];
        var id = empleado["Id"];
        var nombre = "#nombreEC" + id;
        $(nombre).html(empleado["Nombre"]);
    }
}


//Funciones que interactuan con el controlador

function obtenerReparto() {
    $.ajax({
        type: 'GET',
        url: '../../Reparto/ObtenerNuevoReparto',
        dataType: 'json',
        async: false,
        success: function (data) {
            reparto = data;
            console.log(reparto);
        }
    });
}

function enviarReparto() {
    $.ajax({
        type: "POST",
        url: "../../Reparto/IngresarReparto",
        content: "application/json; charset=utf-8",
        dataType: "json",
        //async: false,
        data: { Reparto: reparto },
        success: function (data) {
            repartoCompleto = data;
            console.log(repartoCompleto);
            completarRepartoCompleto();            
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
        }
    });
}


