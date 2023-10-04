// Some terrible js, but im tired with all this js thigs :(
document.getElementById("avatarform").addEventListener("submit", async (event) => {
    event.preventDefault();

    const response = await fetch("/profile/editavatar", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: new FormData(document.getElementById("avatarform"))
    });

    if (!response.ok)
        alert(await response.text())
    location.reload();
});

document.getElementById("usernameform").addEventListener("submit", async (event) => {
    event.preventDefault();

    const response = await fetch("/profile/editusername", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: new FormData(document.getElementById("usernameform"))
    });

    if (!response.ok)
        alert(await response.text())
    location.reload();
});

document.getElementById("phoneform").addEventListener("submit", async (event) => {
    event.preventDefault();

    const response = await fetch("/profile/editphone", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: new FormData(document.getElementById("phoneform"))
    });

    if (!response.ok)
        alert(await response.text())
    location.reload();
});

document.getElementById("emailform").addEventListener("submit", async (event) => {
    event.preventDefault();

    const response = await fetch("/profile/editemail", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: new FormData(document.getElementById("emailform"))
    });

    if (!response.ok)
        alert(await response.text())
    location.reload();
});

document.getElementById("passwordform").addEventListener("submit", async (event) => {
    event.preventDefault();

    const response = await fetch("/profile/editpassword", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: new FormData(document.getElementById("passwordform"))
    });

    if (!response.ok)
        alert(await response.text())
    location.reload();
});

const avatarbutton = document.getElementById("editavatar")
avatarbutton.addEventListener("click", async (event) => {
    event.preventDefault();
    const form = document.getElementById("avatarform");
    form.classList.remove("hidden");
    avatarbutton.classList.add("hidden");
});

const usernamebutton = document.getElementById("editusername")
usernamebutton.addEventListener("click", async (event) => {
    event.preventDefault();
    document.getElementById("usernameform").classList.remove("hidden");
    document.getElementById("usernamespan").classList.add("hidden");
    document.getElementById("usernameinput").value = document.getElementById("username").innerHTML.trim();
    usernamebutton.classList.add("hidden");
});

const phonebutton = document.getElementById("editphone")
phonebutton.addEventListener("click", async (event) => {
    event.preventDefault();
    document.getElementById("phoneform").classList.remove("hidden");
    document.getElementById("phonespan").classList.add("hidden");
    document.getElementById("phoneinput").value = document.getElementById("phone").innerHTML.trim();
    phonebutton.classList.add("hidden");
});

const emailbutton = document.getElementById("editemail")
emailbutton.addEventListener("click", async (event) => {
    event.preventDefault();
    document.getElementById("emailform").classList.remove("hidden");
    document.getElementById("emailspan").classList.add("hidden");
    document.getElementById("emailinput").value = document.getElementById("email").innerHTML.trim();
    emailbutton.classList.add("hidden");
});

const editpassword = document.getElementById("editpassword")
editpassword.addEventListener("click", async (event) => {
    event.preventDefault();
    document.getElementById("passwordform").classList.remove("hidden");
    document.getElementById("passwordspan").classList.add("hidden");
    editpassword.classList.add("hidden");
});