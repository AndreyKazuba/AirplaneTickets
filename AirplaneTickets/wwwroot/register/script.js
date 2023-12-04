const registerButton = document.getElementById('sign-up-button');

registerButton.onclick = async () => {
    const login = document.getElementById('login').value;
    const password = document.getElementById('password').value;
    const passwordConfirmation = document.getElementById('passwordConfirmation').value;
    showSpinner();

    const response = await post('register', {
        login,
        password,
        passwordConfirmation,
    });

    if (response.status == 200) {
        goToFlights();
    }
    if (response.status == 409) {
        const error = document.getElementById('userExist');
        document.getElementById('invalidData').style.display = 'none';
        document.getElementById('passwordConfimationFailed').style.display = 'none';
        error.style.display = 'block';
        hideSpinner();
    }
    if (response.status == 401) {
        const error = document.getElementById('passwordConfimationFailed');
        document.getElementById('invalidData').style.display = 'none';
        document.getElementById('userExist').style.display = 'none';
        error.style.display = 'block';
        hideSpinner();
    }
    if (response.status == 400) {
        const error = document.getElementById('invalidData');
        document.getElementById('userExist').style.display = 'none';
        document.getElementById('passwordConfimationFailed').style.display = 'none';
        error.style.display = 'block';
        hideSpinner();
    }
};

const showSpinner = () => {
    document.getElementById('sign-up-text').style.display = 'none';
    document.getElementById('s-spinner').style.display = 'block';
};

const hideSpinner = () => {
    document.getElementById('sign-up-text').style.display = 'block';
    document.getElementById('s-spinner').style.display = 'none';
};