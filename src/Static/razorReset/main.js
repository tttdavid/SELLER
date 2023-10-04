document.getElementById("form").addEventListener("submit", async (event) => {
    event.preventDefault();

    const response = await fetch("/reset", {
        method: "PUT",
        headers: { "Accept": "application/json" },
        body: new FormData(document.getElementById("form"))
    });

    if (response.ok) {
        alert("New password sent to your email");
        window.location.replace("/");
    }
    else {
        alert("Declined");
    }
});