window.addEventListener("load", async () => {
    await LoadButtons();
});

async function LoadButtons() {
    const cookieName = "SKzgEs5BKE8f1%";
    if (document.cookie.indexOf(cookieName) > -1) {
        await LoadButtonsLoged();
    }
    else {
        await LoadButtonsNonLoged();
    }
};

async function LoadButtonsLoged() {
    const profile = `
    <a href="/profile">
        <button class="nav-button">
            <span class="buttontext">
                My profile
            </span>
        </button>
    </a>`

    const logout = `
    <a href="/logout">
        <button class="nav-button">
            <span class="buttontext">
                Logout
            </span>
        </button>
    </a>`

    document.querySelector("[data-navigation]").insertAdjacentHTML("beforeend", profile)
    document.querySelector("[data-navigation]").insertAdjacentHTML("beforeend", logout)
};

async function LoadButtonsNonLoged() {
    const login = `
    <a href="/login">
        <button class="nav-button">
            <span class="buttontext">
                Login
            </span>
        </button>
    </a>`

    const registration = `
    <a href="/registration">
        <button class="nav-button">
            <span class="buttontext">
                registration
            </span>
        </button>
    </a>`

    document.querySelector("[data-navigation]").insertAdjacentHTML("beforeend", login)
    document.querySelector("[data-navigation]").insertAdjacentHTML("beforeend", registration)
};