// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const imageInput = document.querySelector("#Image");
const image = document.querySelector("#Image_Url");
let lastImageUrl = "";

if (imageInput) {
    imageInput.addEventListener("change", () => {
        if (lastImageUrl) {
            URL.revokeObjectURL(lastImageUrl);
        }
        const filesList = imageInput.files;

        if (filesList.length) {
            lastImageUrl = URL.createObjectURL(filesList[0]);
            image.setAttribute("src", lastImageUrl);
        }
    })
}