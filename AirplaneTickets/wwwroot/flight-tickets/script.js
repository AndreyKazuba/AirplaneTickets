const flightId = localStorage.getItem('current_flight_id');

const addZero = value => {
    if (value >= 10) {
        return value;
    }

    return `0${value}`;
};

document.addEventListener('DOMContentLoaded', async () => {
    let response = await get(`flights/${flightId}`);
    if (response.status == 200) {
        const flight = await response.json();
        const dateValue = new Date(flight.date);
        document.getElementById('date-time').innerText = `${dateValue.toLocaleDateString('ru-RU')} ${addZero(dateValue.getHours())}:${addZero(dateValue.getMinutes())}`;
        document.getElementById('company').innerText = flight.company;
        document.getElementById('flight-name').innerText = flight.name;
        document.getElementById('plane-name').innerText = flight.plane;
        document.getElementById('sub-header').style.transform = 'translateY(0px)';
    }

    response = await get(`flight-tickets/${flightId}`);

    if (response.status == 200) {
        const main = document.getElementById('main');
        const tickets = await response.json();
        main.innerHTML = '';
        document.getElementById('spinner').style.display = 'none';

        for (const ticket of tickets) {
            main.appendChild(createTicketItem(ticket));
        }

        const ticketItems = document.getElementsByClassName('ticket');
        for (let i = 0; i < ticketItems.length; i++) {
            setTimeout(() => {
                setTimeout(() => {
                    ticketItems[i].style.transform = 'translateY(0)';
                    ticketItems[i].style.opacity = 1;
                }, i * 5);
            }, 100);
        }
    }
});

const createTicketItem = ticket => {
    const ticketItem = document.createElement('div');
    ticketItem.classList.add('ticket');

    const seat = document.createElement('div');
    seat.classList.add('seat');
    seat.innerText = ticket.seat;

    const price = document.createElement('div');
    price.classList.add('price');
    price.innerText = `${ticket.rootPrice}-${ticket.rootPrice + 500} BYN`;

    const button = document.createElement('div');
    if (ticket.sold) {
        button.classList.add('sold');
        button.innerText = 'Продано';
    }
    else {
        button.classList.add('buy-button');
        button.innerText = 'Купить';
        button.addEventListener('click', () => {
            localStorage.setItem('current_ticket_id', ticket.id);
            goToBuy();
        });
    }

    ticketItem.appendChild(seat);
    ticketItem.appendChild(price);
    ticketItem.appendChild(button);

    return ticketItem;
};