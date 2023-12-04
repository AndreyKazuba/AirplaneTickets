const loginButton = document.getElementById('log-in-button');

loginButton.onclick = async () => {
    const login = document.getElementById('login').value;
    const password = document.getElementById('password').value;
    showSpinner();

    const response = await post('login', {
        login,
        password,
    });

    if (response.status == 200) {
        goToFlights();
    }

    if (response.status == 401) {
        hideSpinner();
        const error = document.getElementById('error');
        error.style.display = 'block';
    }
};

const showSpinner = () => {
    document.getElementById('login-text').style.display = 'none';
    document.getElementById('s-spinner').style.display = 'block';
};

const hideSpinner = () => {
    document.getElementById('login-text').style.display = 'block';
    document.getElementById('s-spinner').style.display = 'none';
};