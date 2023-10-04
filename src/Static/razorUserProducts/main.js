window.addEventListener("load", async () => {
    const userId = document.getElementById("userId").innerHTML.trim();
    await RequestDataByUser(userId, 0);
});

async function ReqiestPaginatonByUser(userId, pageId)
{
    const response = await fetch(`pagination/${userId}/${pageId}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    const data = await response.json();
    ClearById("pagination");

    data.forEach(i => {
        const btn = document.createElement("button");
        btn.classList.add("pagination-button");
        if (pageId == i) {
            btn.classList.add("active");
        } else {
            btn.addEventListener("click", () => RequestDataByUser(userId, i))
        }
        btn.textContent = i + 1;
        document.getElementById("pagination").appendChild(btn);
    });
}

async function RequestDataByUser(userId, pageId) {
    const response = await fetch(`${userId}/${pageId}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    const data = await response.json();

    ClearById("products");

    data.forEach(item => {
        const div = document.createElement("div");
        div.classList.add("product");
        div.setAttribute("data-product", `${item.id}`);
        div.innerHTML = `
            <div class="product-image-div">
                <img class="product-image" src="../${item.image}" alt="product-image">
            </div>
            <span class="product-title">${item.title}</span>
            <span class="product-price">${item.price}$</span>
        `;

        div.addEventListener("click", () => {
            window.location.href = `/product/${item.id}`;
        });

        document.getElementById("products").appendChild(div);
    });

    ReqiestPaginatonByUser(userId, pageId);
};

function ClearById(id) {
    let elemetn = document.getElementById(id);
    while (elemetn.firstChild) {
        elemetn.removeChild(elemetn.firstChild);
    }
};