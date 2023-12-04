document.addEventListener('DOMContentLoaded', async () => {
    const response = await get('flights');

    if (response.status == 200) {
        const main = document.getElementById('main');
        const flights = await response.json();
        main.innerHTML = '';
        document.getElementById('spinner').style.display = 'none';

        for (const flight of flights) {
            main.appendChild(createFlightItem(flight));
        }

        const flightItems = document.getElementsByClassName('flight');
        for (let i = 0; i < flightItems.length; i++) {
            setTimeout(() => {
                setTimeout(() => { 
                    flightItems[i].style.transform = 'translateY(0)';
                    flightItems[i].style.opacity = 1;
                }, i * 10);
            }, 100);
        }
    }
});

const addZero = value => {
    if (value >= 10) {
        return value;
    }

    return `0${value}`;
};

const createFlightItem = flight => {
    const flightItem = document.createElement('div');
    flightItem.classList.add('flight');

    const image = document.createElement('div');
    image.classList.add('image');
    const img = document.createElement('img');
    img.src = '../img/plane-icon.svg';
    image.appendChild(img);

    const content = document.createElement('div');
    content.classList.add('content');

    const name = document.createElement('p');
    name.classList.add('name');
    name.innerText = flight.name;

    const date = document.createElement('p');
    date.classList.add('time');
    const dateValue = new Date(flight.date);
    date.innerText = `${dateValue.toLocaleDateString('ru-RU')} ${addZero(dateValue.getHours())}:${addZero(dateValue.getMinutes())}`;

    content.appendChild(name);
    content.appendChild(date);

    flightItem.appendChild(image);
    flightItem.appendChild(content);

    flightItem.addEventListener('click', () => {
        localStorage.setItem('current_flight_id', flight.id);
        goToTickets();
    });

    return flightItem;
}