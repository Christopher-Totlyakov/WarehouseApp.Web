document.addEventListener('DOMContentLoaded', () => {
	const messageModal = document.getElementById('messageModal');

	// Добавяне на събитие за показване на модала
	messageModal.addEventListener('show.bs.modal', function (event) {
		const button = event.relatedTarget;
		const url = button.getAttribute('href');

		// Зареждане на съдържанието в модала
		const modalBody = messageModal.querySelector('.modal-body');
		modalBody.innerHTML = 'Loading...';

		fetch(url)
			.then(response => response.text())
			.then(html => {
				modalBody.innerHTML = html;
			})
			.catch(err => {
				modalBody.innerHTML = '<p>Error loading message details.</p>';
			});
	});

	// Добавяне на събитие за скриване на модала
	messageModal.addEventListener('hidden.bs.modal', () => {
		const modalBackdrop = document.querySelector('.modal-backdrop');
		if (modalBackdrop) {
			modalBackdrop.remove(); // Премахване на фоновия слой
		}

		// Изчистване на съдържанието на модала, ако е необходимо
		const modalBody = messageModal.querySelector('.modal-body');
		modalBody.innerHTML = '';
	});
});