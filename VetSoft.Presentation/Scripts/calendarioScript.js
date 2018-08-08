
var meses = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
var laSemana = ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"]
var diasSemana = ["DOM", "LUN", "MAR", "MIE", "JUE", "VIE", "SAB"];
var hoy, diaSemHoy, diaHoy, mesHoy, añoHoy, acCal;
var tituloDom, añoDom, ant, pos, f0, mesCal, añoCal;
var mesAnt, mesPos, primeroMes;
var citasMes = {};


$(document).ready(function () {
    hoy = new Date();
    diaSemHoy = hoy.getDay();
    diaHoy = hoy.getDate();
    mesHoy = hoy.getMonth();
    añoHoy = hoy.getFullYear();


    tituloDom = document.getElementById("currentMonth");
    ant = document.getElementById("prevMonth");
    pos = document.getElementById("nextMonth");


    f0 = document.getElementById("fila0");

    mesCal = mesHoy; //mes principal
    añoCal = añoHoy //año principal

    cabecera();
    primeraLinea();

    CargarCitasArray(mesHoy, añoHoy);
    //escribirDias();
});
var cabecera = function () {
    tituloDom.innerHTML = meses[mesCal] + '<br><span style="font-size:18px">' + añoCal + '</span>';

    mesAnt = mesCal - 1; //mes anterior
    mesPos = mesCal + 1; //mes posterior
    if (mesAnt < 0) { mesAnt = 11; }
    if (mesPos > 11) { mesPos = 0; }
    ant.title = meses[mesAnt];
    pos.title = meses[mesPos];
};
var primeraLinea = function () {
    for (var i = 0; i < 7; i++) {
        var c = f0.getElementsByTagName("th")[i];
        c.innerHTML = diasSemana[i];
    }
};
var escribirDias = function () {
    primeroMes = new Date(añoCal, mesCal, "1");
    prSem = primeroMes.getDay();
    //prSem++; //adaptar al calendario español (empezar por lunes)
    if (prSem == 7) { prSem = 0; }

    diaprmes = primeroMes.getDate();
    prcelda = diaprmes - prSem;
    empezar = primeroMes.setDate(prcelda);
    diames = new Date();
    diames.setTime(empezar);

    for (i = 1; i < 7; i++) {
        fila = document.getElementById("fila" + i);
        for (j = 0; j < 7; j++) {
            midia = diames.getDate()
            mimes = diames.getMonth()
            mianno = diames.getFullYear()
            celda = fila.getElementsByTagName("td")[j];
            celda.innerHTML = midia;
            var citaData = GetCheckData(midia);
            //console.log(checkData);

            if (i % 2 == 0) {
                celda.style.backgroundColor = "#eee";
            } else {
                celda.style.backgroundColor = "#f2f2f2";
            }
            celda.style.color = "#492736";

            if (j == 6) {
                celda.style.color = "#0000ff";
            } else if (j == 0) {
                celda.style.color = "#ff0000";
            }

            if (mimes != mesCal) {
                celda.style.color = "#a0babc";
            }

            if (mimes == mesHoy && midia == diaHoy && mianno == añoHoy) {
                celda.style.backgroundColor = "#b3ffb3";
                celda.innerHTML = "<cite title='Fecha Actual'>" + midia + "</cite>";
            }

            if (citaData != null) {
                celda.innerHTML =
                    celda.innerHTML + 
                    "<br />" +
                    "<a href='" + citaData.Url + "' class='btn' title='Tiene " + citaData.Cantidad + " citas.'> Ver Citas</a>";
            }
            midia = midia + 1;
            diames.setDate(midia);
        }
    }
};

var proximoMes = function () {
    var nuevomes = new Date();
    var tiempounix = primeroMes.getTime();
    tiempounix = tiempounix + (45 * 24 * 60 * 60 * 1000);
    nuevomes.setTime(tiempounix);
    mesCal = nuevomes.getMonth();
    añoCal = nuevomes.getFullYear();
    $.notify("Mes actual: " + meses[mesCal], "info");
    CargarCitasArray(mesCal, añoCal);

    cabecera();
    //escribirDias();
};

var anteriorMes = function () {
    var nuevomes = new Date();
    primeroMes--;
    nuevomes.setTime(primeroMes);
    mesCal = nuevomes.getMonth();
    añoCal = nuevomes.getFullYear();
    $.notify("Mes actual: " + meses[mesCal], "info");
    CargarCitasArray(mesCal, añoCal);

    cabecera();
    //escribirDias();

};

var CargarCitasArray = function (mes, año) {

    mes = mes + 1;
    if (mes > 12)
        mes = 1;
    var opt = {
        type: "GET",
        url: "/Citas/Listar/" + año + "/" + mes,
        success: function (data) {
            citasMes = data;
        },
        error: function (error) {
            console.log(error);
        }
    };
    //$.ajax(opt);
    var url = "/Citas/Listar/" + año + "/" + mes;
    $.getJSON(url, function (data) {
        console.log(data);
        citasMes = data;
        escribirDias();
    });

};

var GetCheckData = function (dia) {
    var obj = null;
    if (citasMes !== undefined && citasMes.length > 0)
        $.each(citasMes, function (i, val) {
            if (val.Dia == dia) {
                obj = {
                    Dia: val.Dia,
                    Cantidad: val.Cantidad,
                    Url: val.Url
                };
            }

        });

    return obj;
};