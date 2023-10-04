window.addEventListener("load", async () => {
    await SetUpImagePreview();

    document.getElementById("author").addEventListener("click", () => {
        const id = document.getElementById("userId").innerHTML.trim();
        window.location.href = `/user/${id}`;
    });

    await SetDeleteButton();
});


async function SetDeleteButton() {
    const deletebtn = document.getElementById("delete");
    if (deletebtn !== null) {
        deletebtn.addEventListener("click", async (event) => {
            event.preventDefault();
            if (confirm("Delete product?")) {
                const id = document.getElementById("productId").innerHTML.trim();
                const response = await fetch(`${id}`, {
                    method: "DELETE",
                    headers: { "Accept": "application/json" }
                });

                if (response.ok)
                    window.location.href = "/";
                else
                    console.log("Error");
            };
        });
    }
};

async function SetUpImagePreview() {
    window.addEventListener('click', (event) => {
        if (event.target === modal) {
            modal.style.display = 'none';
        }
    });

    const modal = document.getElementById('imageModal');

    document.getElementById('img').addEventListener('click', () => {
        const imageUrl = document.getElementById("product-image").src;
        document.getElementById('fullImage').src = imageUrl;

        modal.style.display = 'block';
    });

    document.getElementById("close").addEventListener('click', () => {
        modal.style.display = 'none';
    });
};