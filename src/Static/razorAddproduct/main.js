document.getElementById("form").addEventListener("submit", async (event) => {
    event.preventDefault();

    const response = await fetch("/addproduct", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: new FormData(document.getElementById("form"))
    });

    if (response.ok) {
        window.location.replace("/");
    }
    else {
        alert("Error");
    }
});