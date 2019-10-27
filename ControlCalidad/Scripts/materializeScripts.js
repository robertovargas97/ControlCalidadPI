document.addEventListener('DOMContentLoaded', function () {

    //Material Design initializations for html components
    var elems = document.querySelectorAll('.parallax');
    var instances = M.Parallax.init(elems);

    var elems = document.querySelectorAll('select');
    var instances = M.FormSelect.init(elems);

    var elems = document.querySelectorAll('.modal');
    var instances = M.Modal.init(elems);

    var elems = document.querySelectorAll('.dropdown-trigger');
    var instances = M.Dropdown.init(elems, { coverTrigger: false, closeOnClick: true });

    var elems = document.querySelectorAll('.datepicker');
    var instances = M.Datepicker.init(elems);

    var elems = document.querySelectorAll('.datepicker');
    var instances = M.Datepicker.init(elems, {

        format: 'yyyy-mm-dd',
        showClearBtn: true,
        i18n: {
            done: 'Aceptar',
            cancel: 'Cancelar',
            clear: 'Borrar'
        }


    });
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

        success: function (active) {

            if (active == 'Inactivo') {
                document.getElementById("loading").classList.remove("hide");
                location.href = '/Project/RemoveProject/' + parseInt(id);
            }
            else {
                document.getElementById("activeError").innerHTML = "No puedes eliminar un  proyecto activo... Debe estár inactivo o finalizado.";
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
function validateDate() {
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

//------------------------------------------------Employee Validations----------------------------------------------------
function validateEmployeeName(inputtxt) {
    var letters = /^[a-zA-Z\s]*$/;
    if (inputtxt.value.match(letters)) {
        var err = document.getElementById("employeeNameError");
        err.innerHTML = " ";
    }
    else {
        var err = document.getElementById("employeeNameError");
        err.innerHTML = "<span class=red-text>Digite caracteres validos</span>";

    }
}

function validateEmployeeSurname(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    if (inputtxt.value.match(letters)) {
        var err = document.getElementById("employeeSurnameError1");
        err.innerHTML = " ";
    }
    else {
        var err = document.getElementById("employeeSurnameError1");
        err.innerHTML = "<span class=red-text>Digite caracteres validos</span>";
    }
}
function validateEmployeeSurname2(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    if (inputtxt.value.match(letters)) {
        var err = document.getElementById("employeeSurnameError2");
        err.innerHTML = " ";
    }
    else {
        var err = document.getElementById("employeeSurnameError2");
        err.innerHTML = "<span class=red-text>Digite caracteres validos</span>";
    }
}
function validateEmployeeAge(inputtxt) {
    var letters = /^[a-zA-Z\s-]*$/;
    var err = document.getElementById("employeeAgeError");
    if (inputtxt.value.match(letters)) {
        err.innerHTML = "<span class=red-text>Digite caracteres numericos</span>";
    }
    else {
        if (parseInt(inputtxt.value, 10) > 18 && parseInt(inputtxt.value, 10) < 100) {
            err.innerHTML = " "
        } else {
            err.innerHTML = "<span class=red-text>Digite una edad valida</span>";
        }

    }
}
function validateEmployeeEmail(inputtxt) {
    if (inputtxt.value.includes("@") && (inputtxt.value.includes(".com") || inputtxt.value.includes(".net"))) {
        var err = document.getElementById("employeeEmailError");
        err.innerHTML = "";
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
    }
    else {
        err.innerHTML = `<span class=red-text>Digite valores numericos </span>`;
    }
}
function validateEmployeeID(inputtxt) {
    var letters = /^[0-9]*$/;
    var err = document.getElementById("employeeIDError");
    if (inputtxt.value.match(letters)) {
        err.innerHTML = "";
    }
    else {
        err.innerHTML = `<span class=red-text>Digite un valores numericos </span>`;
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
