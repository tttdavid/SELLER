document.getElementById("form").addEventListener("submit", async (event) => {
    event.preventDefault();

    const response = await fetch("/registration", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: new FormData(document.getElementById("form"))
    });

    if (response.ok) {
        window.location.replace("/");
    }
    else {
        alert("Declined");
    }
});