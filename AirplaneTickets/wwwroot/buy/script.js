const flightId = localStorage.getItem('current_flight_id');
const ticketId = localStorage.getItem('current_ticket_id');
let rootPrice = 0;

const addZero = value => {
    if (value >= 10) {
        return value;
    }

    return `0${value}`;
};

document.addEventListener('DOMContentLoaded', async () => {
    const response = await get(`flight-ticket/${ticketId}`);
    if (response.status == 200) {
        const ticket = await response.json();
        document.getElementById('seat').innerText = ticket.seat;
        document.getElementById('price').innerText = ticket.rootPrice;
        const dateValue = new Date(ticket.date);
        document.getElementById('date-time').innerText = `${dateValue.toLocaleDateString('ru-RU')} ${addZero(dateValue.getHours())}:${addZero(dateValue.getMinutes())}`;
        document.getElementById('company').innerText = ticket.company;
        document.getElementById('flight-name').innerText = ticket.name;
        document.getElementById('plane-name').innerText = ticket.plane;

        rootPrice = ticket.rootPrice;
        document.getElementById('sub-header').style.transform = 'translateY(0px)';

        const buyButton = document.getElementById('buy-button');
        buyButton.addEventListener('click', async () => {
            showSpinner();
            const firstName = document.getElementById('first-name').value;
            const lastName = document.getElementById('last-name').value;
            const body = {
                firstName,
                lastName,
                ticketId: ticket.id,
            };

            const response = await put('flight-tickets', body);
            if (response.status == 200) {
                goToMyTickets();
            }

            if (response.status == 400) {
                hideSpinner();
                const error = document.getElementById('error');
                error.style.display = 'block';
            }
        });
    }
});

setTimeout(() => {
    const main = document.getElementById('main');
    main.style.transform = 'translate(-50%, -50%)';
    main.style.opacity = 1;
}, 10);

function oncheck() {
    const baggageAdd = document.getElementById('baggage').checked ? 250 : 0;
    const queueAdd = document.getElementById('queue').checked ? 250 : 0;
    document.getElementById('price').innerText = rootPrice + baggageAdd + queueAdd;
}

const showSpinner = () => {
    document.getElementById('buy-text').style.display = 'none';
    document.getElementById('s-spinner').style.display = 'block';
};

const hideSpinner = () => {
    document.getElementById('buy-text').style.display = 'block';
    document.getElementById('s-spinner').style.display = 'none';
};