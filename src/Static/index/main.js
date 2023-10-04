window.addEventListener("load", async () => {
    await SetUpGenreButtons();
    await RequestDataForPage("default", 0);
});

async function ReqiestPaginaton(genre, id) {
    const response = await fetch(`api/pagination/${genre}/${id}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    const data = await response.json();
    ClearById("pagination");

    data.forEach(i => {
        const btn = document.createElement("button");
        btn.classList.add("pagination-button");
        if (id == i) {
            btn.classList.add("active");
        } else {
            btn.addEventListener("click", () => RequestDataForPage(genre, i))
        }
        btn.textContent = i + 1;
        document.getElementById("pagination").appendChild(btn);
    });
};

async function RequestDataForPage(genre, id) {
    const response = await fetch(`api/${genre}/${id}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (!response.ok)
    {
        console.log("qwe");
    }

    const data = await response.json();

    ClearById("products");

    data.forEach(item => {
        const div = document.createElement("div");
        div.classList.add("product");
        div.setAttribute("data-product", `${item.id}`);
        div.innerHTML = `
            <div class="product-image-div">
                <img class="product-image" src="${item.image}" alt="product-image">
            </div>
            <span class="product-title">${item.title}</span>
            <span class="product-price">${item.price}$</span>
        `;

        div.addEventListener("click", () => {
            window.location.href = `/product/${item.id}`;
        });

        document.getElementById("products").appendChild(div);
    });

    ReqiestPaginaton(genre, id);
};

document.getElementById("search").addEventListener("submit", (event) => {
    event.preventDefault();

    let genre = document.querySelector(".searchInput").value;
    if (genre.length > 0)
        RequestDataForPage(genre, 0);
    
    document.querySelector(".searchInput").value == "";
});

function ClearById(id) {
    let elemetn = document.getElementById(id);
    while (elemetn.firstChild) {
        elemetn.removeChild(elemetn.firstChild);
    }
};

async function SetUpGenreButtons() {
    var divs = document.querySelectorAll(".option");
    divs.forEach(div => {
        div.addEventListener("click", function () {
            var genre = div.querySelector('p').innerHTML;
            console.log(genre);
            RequestDataForPage(genre, 0);
        });
    });
}