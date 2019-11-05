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

//<summary> :   validates that the user sets a correct duration for the project.
function validateDuration() {
    if (document.getElementById("avrDuration").value.length <= 0) {
        document.getElementById("avrDurationError").innerHTML = "Debes ingresar una duracion válida.";
    }
    else {
        document.getElementById("avrDurationError").innerHTML = "";
    }
}
//------------------------------------------------------------------------------------------------------------------------

//------------------------------------------------Requirements Validations------------------------------------------------
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
function validateRequirementName() {
    $.ajax({
        url: '/Requirement/validateName',
        data: { name: $('#requirementName').val() },
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
    if (document.getElementById("avrDuration").value.length <= 0) {
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
    if (document.getElementById("RealDur").value.length <= 0) {
        document.getElementById("realDurError").innerHTML = "Debes ingresar una duracion real válida.";
        document.getElementById('btn-submit').disabled = true;

    }
    else {
        document.getElementById("realDurError").innerHTML = "";
        document.getElementById('btn-submit').disabled = false;

    }
}

//<summary> :   validates that the user select correct date to the requirement.
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
function validateNameClient() {
    if (document.getElementById("nameClient").value.length <= 0) {
        document.getElementById("nameClientError").innerHTML = "Debes ingresar un nombre.";
    }
    else {
        document.getElementById("nameClientError").innerHTML = "";
    }
}

//<summary> :   validates the surname that will be placed in the input.
function validateSurnameClient() {
    if (document.getElementById("surnameClient").value.length <= 0) {
        document.getElementById("surnameClientError").innerHTML = "Debes ingresar el primer apellido.";
    }
    else {
        document.getElementById("surnameClientError").innerHTML = "";
    }
}

//<summary> :   validates the second surname that will be placed in the input.
function validateSecondSurnameClient() {
    if (document.getElementById("secondSurnameClient").value.length <= 0) {
        document.getElementById("secondSurnameClientError").innerHTML = "Debes ingresar el segundo apellido.";
    }
    else {
        document.getElementById("secondSurnameClientError").innerHTML = "";
    }
}

//<summary> :   validates the client id that will be placed in the input.
//<param>   :   input, represents id of the client to be validated.
function validateIdClient(input) {
    var letters = /^[0-9]*$/;
    if (document.getElementById("idClient").value.length <= 7) {
        document.getElementById("idClientError").innerHTML = "Debes ingresar una cédula válida.";
    }
    else {
        if (input.value.match(letters)) {
            document.getElementById("idClientError").innerHTML = "";
        } else {
            document.getElementById("idClientError").innerHTML = "Debes ingresar una cédula válida.";
        }
    }
}
