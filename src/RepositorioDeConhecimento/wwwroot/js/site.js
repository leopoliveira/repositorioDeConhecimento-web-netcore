﻿//*** EVENTOS ***//

document.querySelectorAll('.nav-link').forEach(function (element) {
    element.addEventListener('click', function (e) {
        document.querySelectorAll('.nav-link').forEach(function (element) {
            element.classList.remove('active');
        });
        e.preventDefault();
        e.target.classList.add('active');
    });
});