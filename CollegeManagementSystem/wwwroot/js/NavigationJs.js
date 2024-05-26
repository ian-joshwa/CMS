$.ajax({
    url: '/Navigation/Get',
    method: 'GET',
    success: function (data) {

        let dataLinks = document.querySelectorAll('.side-link');

        

        dataLinks.forEach(link => {
            if (link.getAttribute('data-link') === data) {
                link.classList.add('side-link-active');
            }
            else {
                if (link.classList.contains('side-link-active')) {
                    link.classList.remove('side-link-active');
                }
            }
        })

    },
    error: function (err) {
        console.error('Error:', err);
    }
});