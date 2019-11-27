document.addEventListener('DOMContentLoaded', function () {

    //Material Design initializations for html components

    var elems = document.querySelectorAll('.tooltipped');
    var instances = M.Tooltip.init(elems);

    var elems = document.querySelectorAll('.parallax');
    var instances = M.Parallax.init(elems);

    var elems = document.querySelectorAll('select');
    var instances = M.FormSelect.init(elems);

    var elems = document.querySelectorAll('.modal');
    var instances = M.Modal.init(elems);

    var elems = document.querySelectorAll('.dropdown-trigger');
    var instances = M.Dropdown.init(elems, { coverTrigger: false, closeOnClick: true });

    var elems = document.querySelectorAll('.datepickerDate');
    var instances = M.Datepicker.init(elems, {
        format: 'yyyy-mm-dd',
        showClearBtn: true,
        i18n: {
            done: 'Aceptar',
            cancel: 'Cancelar',
            clear: 'Borrar'
        }
    });

    var elems = document.querySelectorAll('.datepickerAge');
    var instances = M.Datepicker.init(elems, {
        format: 'yyyy-mm-dd',
        showClearBtn: true,
        i18n: {
            done: 'Aceptar',
            cancel: 'Cancelar',
            clear: 'Borrar'
        },
        yearRange: [1940, 2001]
    });

    var elems = document.querySelectorAll('.fixed-action-btn');
    var instances = M.FloatingActionButton.init(elems);

});

//  <summary> : redirect to specific address
//  <param> : the address to redirect
function redirectToPage(address) {
    location.href = address;
}

// <sumary> : show a loading component to give feedback to the user
function onSubmit() {
    document.getElementById("loading").classList.remove("hide");
}

//-----------------------------------Reports functions--------------------------------------

function getFinishedProjectInfo() {
    var idProject = document.getElementById("finisehdProjects").value;
    $("#projectHoursRequirementsTable").empty();
    $.get("/Reports/ProjectRequirementesHours", { projectId: idProject }, function (data) {
        $.each(data, function (index, info) {
            $("#projectHoursRequirementsTable").append("<tr><td class='center'>" + info.Horas_estimadas + "</td><td class='center'>" + info.Horas_reales + "</td><td class='center'>" + info.Requerimientos_exitosos + "</td><td class='center'>" + info.Requerimientos_fallidos + "</td>" + "<td class='center'>" + info.Requerimientos_Totales + "</td><tr>");
            showCharts(info.Horas_estimadas, info.Horas_reales, info.Requerimientos_fallidos, info.Requerimientos_exitosos);
        });
    });

}

function showCharts(avgHours, realHours, failedRequirements, successfulRequirements) {
    //Load the Visualization API and the corechart package.
    google.charts.load('current', { 'packages': ['corechart'] });
    //Set a callback to run when the Google Visualization API is loaded.
    drawBarChart(avgHours, realHours, "chart_project_div","Horas","Horas Estimadas","Horas Reales","Horas del proyecto",1);
    drawBarChart(failedRequirements, successfulRequirements, "chart_project_div_req", "Requerimientos", "Fallidos","Exitosos","Requerimientos del proyecto",2);
}

function drawBarChart(data1, data2,div,name,dataLabel1,dataLabel2,tittle,option) {
    var data = google.visualization.arrayToDataTable([
        [name, '', { role: 'style' }, { role: 'annotation' }],
        [dataLabel1, parseInt(data1), 'stroke-color: #DE3910; stroke-width: 2; fill-color: #DE3910', data1],
        [dataLabel2, parseInt(data2), 'stroke-color: #1272C6; stroke-width: 2; fill-color: #1272C6', data2]
    ]);

    var options = {
        title: tittle,
        is3D: true,
    };

    if (option == 1) {
        var chart = new google.visualization.ColumnChart(document.getElementById(div));

    }
    else if (option == 2) {
        var chart = new google.visualization.PieChart(document.getElementById(div));
    }

    chart.draw(data, options);
}






//------------------------------------Validation functions for inputs-----------------------

//--------------------------------------Register Validations-------------------------------

//<summary> :   validates the email that will be placed in the inputs.
//<param>   : idElement,represents the id of the html input to be validated.
function validateEmail(idElement) {

    if (document.getElementById(idElement).value.length < 3) {
        document.getElementById("mailError").innerHTML = "Debe introducir un correo válido";
    }

    else {
        if ((document.getElementById(idElement).value.indexOf("@") == -1) || (document.getElementById(idElement).value.indexOf(".") == -1)) {
            document.getElementById("mailError").innerHTML = "Debe introducir un correo válido";
        }
        else {
            document.getElementById("mailError").innerHTML = "";
            existMail();
        }
    }
}

//<summary> :   validates if the mail placed existe in the database using ajax.
function existMail() {
    $.ajax({
        url: '/LoginUsers/validateEmail',
        data: { mail: $('#Email').val() },
        success: function (exist) {
            if (exist == 'True') {
                document.getElementById("mailError").innerHTML = "El correo ya existe... Por favor ingrese otro";
                document.getElementById('btn-submit').disabled = true;
            }
            else {
                document.getElementById("mailError").innerHTML = "";
                document.getElementById('btn-submit').disabled = false;
            }
        },
    });
}

//<summary> :   validates the password that will be placed in the inputs.
//<param>   : password,represents the id of the html input to be validated.
function validatePassword(password) {
    if (document.getElementById(password).value.length < 4) {
        document.getElementById("passwordError").innerHTML = "La contraseña debe tener 5 caracter como mínimo.";
    }
    else {
        document.getElementById("passwordError").innerHTML = "";
    }
}

//<summary> :   validates that the password and the confirm password are the same.
function validateConfirmPass() {
    let password = document.getElementById("Password").value;
    let passConfirm = document.getElementById("ConfirmPassword").value;

    if (password != passConfirm) {
        document.getElementById("passwordConfirmError").innerHTML = "Las contraseñas no coinciden.";
    }
    else {
        document.getElementById("passwordConfirmError").innerHTML = "";
    }
}
//----------------------------------------------------------------------------------------------


//-------------------------------------------Project Validations--------------------------------

//<summary> :   validates that a project name has a minimun length and that project does not exist in the db
function validateProjectName() {

    if (document.getElementById("projectName").value.length < 5) {
        document.getElementById("nameError").innerHTML = "Debe colocar un nombre válido al proyecto(5 caracteres como mínimo).";
    }
    else {
        document.getElementById("nameError").innerHTML = "";
        console.log("acv");
        $.ajax({
            url: '/Project/validateName',
            data: { name: $('#projectName').val() },
            success: function (exist) {
                if (exist == 'True') {
                    document.getElementById("nameError").innerHTML = "El nombre del proyecto ya existe... Por favor ingrese otro";
                    document.getElementById('btn-submit').disabled = true;
                }
                else {
                    document.getElementById("nameError").innerHTML = "";
                    document.getElementById('btn-submit').disabled = false;
                }
            },
        });
    }
}

//<summary> : validates the status of a project,if it status is active the project can not be deleted (using ajax)
function removeProject() {
    var id = document.getElementById("idProject").value;
    $.ajax({
        url: '/Project/activeProject',
        data: { id: $('#idProject').val() },

        success: function (cancelled) {

            if (cancelled == "Cancelado") {
                document.getElementById("loading").classList.remove("hide");
                location.href = '/Project/RemoveProject/' + parseInt(id);
            }
            else {
                document.getElementById("activeError").innerHTML = "Solo puedes eliminar un proyecto con el estado cancelado...Por favor cambia el estado.";
            }
        },
    });
}

//<summary> :   validates that a client is selected
//<param>   : idClient,represents the id of the html input to be validated.
function validateClient(idClient) {
    if (document.getElementById(idClient).value.indexOf("Selecciona el cliente") > -1) {
        document.getElementById("ClientError").innerHTML = "Debe seleccionar un cliente.";
    }
}

//<summary> :   validates that the user select a begin date to the project.
function validateDateP() {
    if (document.getElementById("fechaInicio").value.length <= 0) {
        document.getElementById("dateErrorMessage").innerHTML = "Debe seleccionar una fecha de inicio para el proyecto.";
    }
    else {
        document.getElementById("dateErrorMessage").innerHTML = "";
    }
}

//------------------------------------------------------------------------------------------------------------------------

//------------------------------------------------Requirement Validations------------------------------------------------
//<summary> :   validates if a requirement can be deleted. It can deleted be if does not related with any tester or test.
//<param>   : id (requirement id) and projectId.
function deleteRequirement(id, projectId) {
    $.ajax({
        url: '../../TesterRequirement/canDelete/',
        data: { id: id },

        success: function (canDelete) {

            if (canDelete == 'True') {
                document.getElementById("loading").classList.remove("hide");
                location.href = '/Requirement/removeRequirement/' + parseInt(id) + '?projectId=' + parseInt(projectId);
            }
            else {
                document.getElementById("deleteError").innerHTML = "El requerimiento deber estar libre para poder eliminarlo... Por favor termine las pruebas y/o desasocie el tester.";
            }
        },
    });
}

//<summary> :   validates that a requirement does not exist in the db
//<param>   : id (requirement id).
function validateRequirementName(id) {
    $.ajax({
        url: '/Requirement/validateName',
        data: { name: $('#requirementName').val(), idProyect: id },
        success: function (exist) {
            if (exist == 'True') {
                document.getElementById("nameError").innerHTML = "El nombre del requerimiento ya existe... Por favor ingrese otro";
                document.getElementById('btn-submit').disabled = true;
            }
            else {
                document.getElementById("nameError").innerHTML = "";
                document.getElementById('btn-submit').disabled = false;
            }
        },
    });
}

//<summary> :   validates that the user select a begin date to the requirement.
function validateDate() {
    if (document.getElementById("fechaInicio").value.length <= 4) {
        document.getElementById("dateErrorMessage").innerHTML = "Debe seleccionar una fecha de inicio para el requerimiento.";
        document.getElementById('btn-submit').disabled = true;

    }
    else {
        document.getElementById("dateErrorMessage").innerHTML = "";
        document.getElementById("endDateError").innerHTML = "";
        document.getElementById("assigndateError").innerHTML = "";

        document.getElementById('btn-submit').disabled = false;

    }
}

//<summary> :   validates that the user sets a correct duration for the project.
function validateDuration() {
    if (document.getElementById("avrDuration").value.length <= 0 || document.getElementById("avrDuration").value <= 0) {
        document.getElementById("avrDurationError").innerHTML = "Debes ingresar una duracion válida.";
        document.getElementById('btn-submit').disabled = true;

    }
    else {
        document.getElementById("avrDurationError").innerHTML = "";
        document.getElementById('btn-submit').disabled = false;

    }
}

//<summary> :   validates that the user sets a correct duration for the project.
function validateRealDuration() {
    if (document.getElementById("RealDur").value.length > 0) {

        if (document.getElementById("RealDur").value <= 0) {
            document.getElementById("realDurError").innerHTML = "Debes ingresar una duracion real válida.";
            document.getElementById('btn-submit').disabled = true;

        }
    }
    else {
        document.getElementById("realDurError").innerHTML = "";
        document.getElementById('btn-submit').disabled = false;

    }
}

//<summary> :   validates that the user select correct date to the requirement.
//<param>   : input is the html id of the input, error is the html id for the error span .
function validateEndAssignDate(input, error) {
    if (document.getElementById("fechaInicio").value.length <= 0 && document.getElementById(input).value.length > 0) {
        document.getElementById(error).innerHTML = "No has ingresado una fecha de inicio para poder colcar esta fecha.";
        document.getElementById('btn-submit').disabled = true;
    }
    else if (document.getElementById(input).value < document.getElementById("fechaInicio").value) {
        document.getElementById(error).innerHTML = "La fecha debe ser posterior o igual  la fecha de inicio.";
        document.getElementById('btn-submit').disabled = true;

    }
    else {
        document.getElementById(error).innerHTML = "";
        document.getElementById('btn-submit').disabled = false;

    }
}



//------------------------------------------------Employee Validations----------------------------------------------------
function validateEmployeeName(inputtxt) {
    var letters = /^[a-zA-Z\s]*$/;
    if (inputtxt.value.match(letters)) {
        var err = document.getElementById("employeeNameError");
        err.innerHTML = " ";
        document.getElementById('btn-submit').disabled = false;
    }
    else {
        var err = document.getElementById("employeeNameError");
        err.innerHTML = "<span class=red-text>Digite caracteres validos</span>";
        document.getElementById('btn-submit').disabled = true;

    }
}

function validateEmployeeSurname(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    if (inputtxt.value.match(letters)) {
        var err = document.getElementById("employeeSurnameError1");
        err.innerHTML = " ";
        document.getElementById('btn-submit').disabled = false;
    }
    else {
        var err = document.getElementById("employeeSurnameError1");
        err.innerHTML = "<span class=red-text>Digite caracteres validos</span>";
        document.getElementById('btn-submit').disabled = true;
    }
}
function validateEmployeeSurname2(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    if (inputtxt.value.match(letters)) {
        var err = document.getElementById("employeeSurnameError2");
        err.innerHTML = " ";
        document.getElementById('btn-submit').disabled = false;
    }
    else {
        var err = document.getElementById("employeeSurnameError2");
        err.innerHTML = "<span class=red-text>Digite caracteres validos</span>";
        document.getElementById('btn-submit').disabled = true;
    }
}
function validateEmployeeAge(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    var err = document.getElementById("employeeAgeError");
    if (inputtxt.value.match(letters)) {
        err.innerHTML = "<span class=red-text>Digite caracteres numericos</span>";
        document.getElementById('btn-submit').disabled = true;
    }
    else {
        if (parseInt(inputtxt.value, 10) > 18 && parseInt(inputtxt.value, 10) < 100) {
            err.innerHTML = " "
            document.getElementById('btn-submit').disabled = false;
        } else {
            err.innerHTML = "<span class=red-text>Digite una edad valida</span>";
            document.getElementById('btn-submit').disabled = true;
        }

    }
}
function validateEmployeeEmail(inputtxt) {
    if (inputtxt.value.includes("@") && (inputtxt.value.includes(".com") || inputtxt.value.includes(".net"))) {
        var err = document.getElementById("employeeEmailError");
        $.ajax({
            url: '/Employee/isMailTaken',
            data: { id: inputtxt.value },

            success: function (exist) {

                if (exist == 'True') {
                    err.innerHTML = `<span class=red-text>Correo previamente registrado </span>`;
                    document.getElementById('btn-submit').disabled = true;

                } else {
                    err.innerHTML = "";
                    document.getElementById('btn-submit').disabled = false;
                }
            },
        });
    } else {
        var err = document.getElementById("employeeEmailError");
        err.innerHTML = "<span class=red-text>Digite un correo valido</span>";
    }
}
function validateEmployeePhoneNumber(inputtxt) {
    var letters = /^[0-9]*$/;
    var err = document.getElementById("employeePhoneError");
    if (inputtxt.value.match(letters)) {
        err.innerHTML = "";
        document.getElementById('btn-submit').disabled = false;
    }
    else {
        err.innerHTML = `<span class=red-text>Digite valores numericos </span>`
        document.getElementById('btn-submit').disabled = true;

    }
}
function validateEmployeeID(inputtxt) {
    var letters = /^[0-9]*$/;
    var err = document.getElementById("employeeIDError");
    if (inputtxt.value.match(letters)) {
        $.ajax({
            url: '/Employee/existID',
            data: { id: inputtxt.value },

            success: function (exist) {

                if (exist == 'True') {
                    err.innerHTML = `<span class=red-text>Cedula previamente registrada </span>`;
                    document.getElementById('btn-submit').disabled = true;

                } else {
                    err.innerHTML = "";
                    document.getElementById('btn-submit').disabled = false;
                }
            },
        });

    }
    else {
        err.innerHTML = `<span class=red-text>Digite un valores numericos </span>`;
        inputtxt.value = inputtxt.value.substring(0, inputtxt.value.length)
    }
}

//------------------------------------------------------------------------------------------------------------------------

//------------------------------------------------Client Validations----------------------------------------------------

//<summary> :   validates the name of the client that will be placed in the input.
//<param>   :   inputttxt, represents the name of the client to be validated.
function validateNameClient(inputtxt) {
    var letters = /^[a-zA-Z\s]*$/;
    if (document.getElementById("nameClient").value.length <= 0) {
        document.getElementById("nameClientError").innerHTML = "Debe ingresar el nombre.";
        document.getElementById('btn-submit').disabled = true;
    }
    else {
        if (inputtxt.value.match(letters)) {
            var err = document.getElementById("nameClientError");
            err.innerHTML = " ";
            document.getElementById('btn-submit').disabled = false;
        }
        else {
            var err = document.getElementById("nameClientError");
            err.innerHTML = "Digite caracteres válidos.";
            document.getElementById('btn-submit').disabled = true;

        }
    }
}

//<summary> :   validates the surname that will be placed in the input.
//<param>   :   inputttxt, represents the surname of the client to be validated.
function validateSurnameClient(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    if (document.getElementById("surnameClient").value.length <= 0) {
        document.getElementById("surnameClientError").innerHTML = "Debe ingresar el primer apellido.";
        document.getElementById('btn-submit').disabled = true;
    }
    else {
        if (inputtxt.value.match(letters)) {
            var err = document.getElementById("surnameClientError");
            err.innerHTML = " ";
            document.getElementById('btn-submit').disabled = false;
        }
        else {
            var err = document.getElementById("surnameClientError");
            err.innerHTML = "Digite caracteres válidos.";
            document.getElementById('btn-submit').disabled = true;
        }
    }
}

//<summary> :   validates the second surname that will be placed in the input.
//<param>   :   inputttxt, represents second surname of the client to be validated.
function validateSecondSurnameClient(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    if (document.getElementById("secondSurnameClient").value.length <= 0) {
        document.getElementById("secondSurnameClientError").innerHTML = "Debe ingresar el segundo apellido.";
        document.getElementById('btn-submit').disabled = true;
    }
    else {
        if (inputtxt.value.match(letters)) {
            var err = document.getElementById("secondSurnameClientError");
            err.innerHTML = " ";
            document.getElementById('btn-submit').disabled = false;
        }
        else {
            var err = document.getElementById("secondSurnameClientError");
            err.innerHTML = "Digite caracteres válidos.";
            document.getElementById('btn-submit').disabled = true;
        }
    }
}


//<summary> :   validates the phone number that will be placed in the input.
//<param>   :   inputttxt, represents the phone number of the client to be validated.
function validateClientPhoneNumber(inputtxt) {
    var letters = /^[0-9]*$/;
    var err = document.getElementById("clientPhoneError");
    if (inputtxt.value.match(letters)) {
        err.innerHTML = "";
        document.getElementById('btn-submit').disabled = false;
    }
    else {
        err.innerHTML = "Digite valores numéricos."
        document.getElementById('btn-submit').disabled = true;

    }
}

//<summary> :   validates the mail that will be placed in the input.
//<param>   :   inputttxt, represents the mail of the client to be validated.
function validateClientEmail(inputtxt) {
    if (document.getElementById("clientEmail").value.length <= 0) {
        document.getElementById("clientEmailError").innerHTML = "Debe ingresar el correo.";
        document.getElementById('btn-submit').disabled = true;
    } else {
        if (inputtxt.value.includes("@") && (inputtxt.value.includes("."))) {
            var err = document.getElementById("clientEmailError");
            $.ajax({
                url: '/Client/isMailTaken',
                data: { input: inputtxt.value },

                success: function (exist) {

                    if (exist == 'True') {
                        err.innerHTML = "Correo previamente registrado";
                        document.getElementById('btn-submit').disabled = true;

                    } else {
                        err.innerHTML = "";
                        document.getElementById('btn-submit').disabled = false;
                    }
                },
            });
        } else {
            var err = document.getElementById("clientEmailError");
            err.innerHTML = "Digite un correo válido.";
        }
    }
}

//<summary> :   validates the client id that will be placed in the input.
//<param>   :   inputttxt, represents the id of the client to be validated.
function validateIdClient(inputtxt) {
    var letters = /^[0-9]*$/;
    var tam = document.getElementById("idClient").value.length;
    var err = document.getElementById("idClientError"); idClientError
    if (document.getElementById("idClient").value.length <= 0) {
        document.getElementById("idClientError").innerHTML = "Debe ingresar la cédula.";
        document.getElementById('btn-submit').disabled = true;
    } else {
        if (inputtxt.value.match(letters)) {
            if (((tam < 8) && (tam > 0)) || (tam > 15)) {
                err.innerHTML = "Ingrese entre 8 y 15 dígitos.";
                document.getElementById('btn-submit').disabled = true;
            } else {
                $.ajax({
                    url: '/Client/existID',
                    data: { id: inputtxt.value },

                    success: function (exist) {
                        if (exist == 'True') {
                            err.innerHTML = "Cédula previamente registrada.";
                            document.getElementById('btn-submit').disabled = true;

                        } else {
                            err.innerHTML = "";
                            document.getElementById('btn-submit').disabled = false;
                        }
                    },
                });
            }
        }
        else {
            err.innerHTML = "Digite valores numéricos.";
            document.getElementById('btn-submit').disabled = true;
        }
    }
}


//------------------------------------------------------------------------------------------------------------------------

//------------------------------------------------Reports Functions----------------------------------------------------

//<summary> : Shows the information of the completed requirements from one project.
function secondReport() {
    var proy2 = document.getElementById('proy2').value;
    $("#table2").empty();
    $.get("/Reports/CompletedRequirements", { proy: proy2 }, function (data) {
        $.each(data, function (index, v2) {
            $("#table2").append("<tr><td>" + v2.nombreRequerimiento + "</td><td>" + v2.estadoRequerimiento + "</td><td>" + v2.complejidadRequerimiento + "</td><td>" + v2.nombreResponsable + "</td><tr>");
        });
    });
}

//<summary> : Shows the information of the running requirements from one project.
function thirdReport() {
    var proy3 = document.getElementById('proy3').value;
    $("#table3").empty();
    $.get("/Reports/RunningRequirements", { proy: proy3 }, function (data) {
        $.each(data, function (index, v3) {
            $("#table3").append("<tr ><td>" + v3.nombreReq + "</td><td>" + v3.complejidadReq + "</td><td>" + v3.nombreResp + "</td>");
        });
    });
}


//<summary> : Shows the information of the quantity of requirements for tester in one project.
function fourthReport() {
    var proy4 = document.getElementById('proy4').value
    $("#table4").empty();
    $("#chart_div").empty();
    $.get("/Reports/TesterRequirements", { proy: proy4 }, function (data) {
        var table = [['Elemento', 'Cantidad', { role: 'style' }]];
        var temp = "";
        $.each(data, function (index, v4) {
            $("#table4").append("<tr><td>" + v4.nombreTester + "</td><td>" + v4.nombreR + "</td><td>" + v4.complejidad + "</td><td>");
            table.push([v4.nombreTester, v4.cantidadReqAsignados, 'blue']);
            if (temp == "") { temp = v4.nombreTester }
            else {
                if (v4.nombreTester == temp) {
                    var ca = table.pop();
                } else { temp = v4.nombreTester }

            }

        });
        var data = google.visualization.arrayToDataTable(table);
        var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
        chart.draw(data);
    });
}

//<summary> : Shows the information of the participation history of one tester in diferents projects.
function fifthReport() {
    var tester = document.getElementById('tester').value
    $("#table5").empty();
    $.get("/Reports/ParticipationHistory", { tester: tester }, function (data) {
        $.each(data, function (index, v5) {
            $("#table5").append("<tr><td>" + v5.proyecto + "</td><td>" + v5.estado + "</td><td>" + v5.Requerimiento + "</td><td>" + v5.estadoReq + "</td><td>" + v5.complejidad + "</td><td>" + dateFormat(new Date(parseInt((v5.fechaInicio).match(/\d+/)[0]))) + "</td><td>" + dateFormat(new Date(parseInt((v5.fechaFinalizacion).match(/\d+/)[0]))) + "</td><td>" + v5.duracionReal + "</td></tr>");
        });
    });
}

//<summary> : Changes the date format.
function dateFormat(d) {
    console.log(d);
    return ((d.getMonth() + 1) + "").padStart(2, "0")
        + "/" + (d.getDate() + "").padStart(2, "0")
        + "/" + d.getFullYear();
}


function showProjects2() {
    document.getElementById('barra2').style.display = "block";
    document.getElementById('barra3').style.display = "none";
    document.getElementById('barra4').style.display = "none";
    document.getElementById('barra5').style.display = "none";
    document.getElementById('barra6').style.display = "none";
    document.getElementById('barra7').style.display = "none";
    document.getElementById('barra8').style.display = "none";
    document.getElementById('consulta2').style.display = "block";
    document.getElementById('consulta3').style.display = "none";
    document.getElementById('consulta4').style.display = "none";
    document.getElementById('consulta5').style.display = "none";
    document.getElementById('consulta6').style.display = "none";
    document.getElementById('consulta7').style.display = "none";
    document.getElementById('consulta8').style.display = "none";
}

function showProjects3() {
    document.getElementById('barra2').style.display = "none";
    document.getElementById('barra3').style.display = "block";
    document.getElementById('barra4').style.display = "none";
    document.getElementById('barra5').style.display = "none";
    document.getElementById('barra6').style.display = "none";
    document.getElementById('barra7').style.display = "none";
    document.getElementById('barra8').style.display = "none";
    document.getElementById('consulta2').style.display = "none";
    document.getElementById('consulta3').style.display = "block";
    document.getElementById('consulta4').style.display = "none";
    document.getElementById('consulta5').style.display = "none";
    document.getElementById('consulta6').style.display = "none";
    document.getElementById('consulta7').style.display = "none";
    document.getElementById('consulta8').style.display = "none";
}

function showProjects4() {
    document.getElementById('barra2').style.display = "none";
    document.getElementById('barra3').style.display = "none";
    document.getElementById('barra4').style.display = "block";
    document.getElementById('barra5').style.display = "none";
    document.getElementById('barra6').style.display = "none";
    document.getElementById('barra7').style.display = "none";
    document.getElementById('barra8').style.display = "none";
    document.getElementById('consulta2').style.display = "none";
    document.getElementById('consulta3').style.display = "none";
    document.getElementById('consulta4').style.display = "block";
    document.getElementById('consulta5').style.display = "none";
    document.getElementById('consulta6').style.display = "none";
    document.getElementById('consulta7').style.display = "none";
    document.getElementById('consulta8').style.display = "none";
}

function showProjects5() {
    document.getElementById('barra2').style.display = "none";
    document.getElementById('barra3').style.display = "none";
    document.getElementById('barra4').style.display = "none";
    document.getElementById('barra5').style.display = "block";
    document.getElementById('barra6').style.display = "none";
    document.getElementById('barra7').style.display = "none";
    document.getElementById('barra8').style.display = "none";
    document.getElementById('consulta2').style.display = "none";
    document.getElementById('consulta3').style.display = "none";
    document.getElementById('consulta4').style.display = "none";
    document.getElementById('consulta5').style.display = "block";
    document.getElementById('consulta6').style.display = "none";
    document.getElementById('consulta7').style.display = "none";
    document.getElementById('consulta8').style.display = "none";
}

function showProjects6() {
    document.getElementById('barra2').style.display = "none";
    document.getElementById('barra3').style.display = "none";
    document.getElementById('barra4').style.display = "none";
    document.getElementById('barra5').style.display = "none";
    document.getElementById('barra6').style.display = "block";
    document.getElementById('barra7').style.display = "none";
    document.getElementById('barra8').style.display = "none";
    document.getElementById('consulta2').style.display = "none";
    document.getElementById('consulta3').style.display = "none";
    document.getElementById('consulta4').style.display = "none";
    document.getElementById('consulta5').style.display = "none";
    document.getElementById('consulta6').style.display = "block";
    document.getElementById('consulta7').style.display = "none";
    document.getElementById('consulta8').style.display = "none";
}

function showProjects7() {
    document.getElementById('barra2').style.display = "none";
    document.getElementById('barra3').style.display = "none";
    document.getElementById('barra4').style.display = "none";
    document.getElementById('barra5').style.display = "none";
    document.getElementById('barra6').style.display = "none";
    document.getElementById('barra7').style.display = "block";
    document.getElementById('barra8').style.display = "none";
    document.getElementById('consulta2').style.display = "none";
    document.getElementById('consulta3').style.display = "none";
    document.getElementById('consulta4').style.display = "none";
    document.getElementById('consulta5').style.display = "none";
    document.getElementById('consulta6').style.display = "none";
    document.getElementById('consulta7').style.display = "block";
    document.getElementById('consulta8').style.display = "none";
}

function showProjects8() {
    document.getElementById('barra2').style.display = "none";
    document.getElementById('barra3').style.display = "none";
    document.getElementById('barra4').style.display = "none";
    document.getElementById('barra5').style.display = "none";
    document.getElementById('barra6').style.display = "none";
    document.getElementById('barra7').style.display = "none";
    document.getElementById('barra8').style.display = "block";
    document.getElementById('consulta2').style.display = "none";
    document.getElementById('consulta3').style.display = "none";
    document.getElementById('consulta4').style.display = "none";
    document.getElementById('consulta5').style.display = "none";
    document.getElementById('consulta6').style.display = "none";
    document.getElementById('consulta7').style.display = "none";
    document.getElementById('consulta8').style.display = "block";
}