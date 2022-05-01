imageInput.addEventListener('change', event => {
    const [file] = imageInput.files
    if (file) {
        previewImage.src = URL.createObjectURL(file)
    }
});