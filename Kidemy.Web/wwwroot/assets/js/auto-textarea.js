



const textAreas = document.querySelectorAll('#autoheighttextarea');

textAreas.forEach(textArea => {
    textArea.addEventListener('input', () => {
        textArea.style.height = 'auto';
        textArea.style.height = textArea.scrollHeight + 'px';
    });
});


