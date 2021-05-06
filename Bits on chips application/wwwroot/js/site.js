// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showPassword() {
  var x = document.getElementById("password");
  if (x.type === "password") {
    x.type = "text";
  } else {
    x.type = "password";
  }
}
const open = document.getElementById('open');
const close = document.getElementById('close');
const modal = document.getElementById('modal_container');
open.addEventListener('click', () => {
    modal.classList.add('show');
});
close.addEventListener('click', () => {
    modal.classList.remove('show');
});