const logOutButton = document.getElementById('log-out');
logOutButton.onclick = async () => {
    const response = await post('logout');

    if (response.status == 200) {
        goToLogin();
    }
};