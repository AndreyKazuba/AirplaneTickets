const addZero = value => {
    if (value >= 10) {
        return value;
    }

    return `0${value}`;
};

document.addEventListener('DOMContentLoaded', async () => {
    const response = await get('my-tickets');
    if (response.status == 200) {
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
                }, i * 10);
            }, 40);
        }
    }
});

const createTicketItem = ticket => {
    const ticketItem = document.createElement('div');
    ticketItem.classList.add('ticket');

    const flight = document.createElement('div');
    flight.classList.add('flight');
    flight.innerText = ticket.name;
    
    const date = document.createElement('div');
    date.classList.add('date-time');
    const dateValue = new Date(ticket.date);
    date.innerText = `${dateValue.toLocaleDateString('ru-RU')} ${addZero(dateValue.getHours())}:${addZero(dateValue.getMinutes())}`;

    const seat = document.createElement('div');
    seat.classList.add('seat');
    seat.innerText = ticket.seat;

    const firstName = document.createElement('div');
    firstName.classList.add('first-name');
    firstName.innerText = ticket.firstName;

    const lastName = document.createElement('div');
    lastName.classList.add('last-name');
    lastName.innerText = ticket.lastName;

    ticketItem.appendChild(flight);
    ticketItem.appendChild(date);
    ticketItem.appendChild(seat);
    ticketItem.appendChild(firstName);
    ticketItem.appendChild(lastName);

    return ticketItem;
};