const tblBlogs = "tblBlogs";
let _blogId = "";

runBlog();

function generateData(row) {
    for (let i = 0; i < row; i++) {
        let no = i + 1;
        createBlog(`title ${no}`, `author ${no}`, `content ${no}`);
    }
}

function runBlog() {
    // createBlog('title', 'author', 'content');
    readBlog();
    // editBlog('316edac7-91dd-4cbe-9ca8-59ae554f7adf');
    // editBlog('0');
    // deleteBlog("0");
    // deleteBlog("28503846-8742-4623-ba07-b279f8c77831");
    // updateBlog("1fc02912-6166-4593-ae9f-17245ed2fc81", "t", "a", "c");
    // updateBlog("0", "t", "a", "c");
    // const id = prompt("Enter ID");
    // const title = prompt("Enter title");
    // const author = prompt("Enter author");
    // const content = prompt("Enter content");
    // updateBlog(id, title, author, content);
}

function readBlog() {
    $("#dataTable").html("");

    let listBlog = getBlogs();

    let htmlRow = "";

    if (listBlog.length === 0) {
        // console.log("No data found");
        return;
    }

    for (let i = 0; i < listBlog.length; i++) {
        const element = listBlog[i];
        // console.log(element.Title);
        // console.log(element.Author);
        // console.log(element.Content);
        // console.log("*****");

        htmlRow += `
                    <tr>
                        <td>
                            <button type="button" class="btn btn-warning" onclick="editBlog('${element.Id
            }')">Edit</button>
                            <button type="button" class="btn btn-danger" onclick="deleteBlog('${element.Id
            }')">Delete</button>
                        </td>
                        <th scope="row">${i + 1}</th>
                        <td>${element.Title}</td>
                        <td>${element.Author}</td>
                        <td>${element.Content}</td>
                    </tr>
                   `;
    }
    // console.log(htmlRow);
    $("#dataTable").html(htmlRow);
}

function createBlog(title, author, content) {
    let listBlog = getBlogs();
    const blog = {
        Id: uuidv4(),
        Title: title,
        Author: author,
        Content: content,
    };

    listBlog.push(blog);
    setLocalStorage(listBlog);
}

function editBlog(id) {
    let listBlog = getBlogs();

    let lst = listBlog.filter((x) => x.Id === id); // return array
    // console.log(lst);
    if (lst.length === 0) {
        console.log("No data found");
        return;
    }
    let item = lst[0];
    console.log(item);

    $("#title").val(item.Title);
    $("#author").val(item.Author);
    $("#content").val(item.Content);

    _blogId = item.Id;
}

function updateBlog(id, title, author, content) {
    let listBlog = getBlogs();
    let itemList = listBlog.filter((x) => x.Id === id);
    if (itemList.length === 0) {
        console.log("No data found");
        return;
    }
    let index = listBlog.findIndex((x) => x.Id === id);
    listBlog[index] = {
        Id: id,
        Title: title,
        Author: author,
        Content: content,
    };
    setLocalStorage(listBlog);
}

function deleteBlog(id) {
    // let result = confirm("Are you sure want to delete?");
    // if (!result) return;

    // let listBlog = getBlogs();
    // let itemList = listBlog.filter((x) => x.Id === id);
    // if (itemList.length === 0) {
    //     console.log("No data found");
    //     return;
    // }
    // listBlog = listBlog.filter((x) => x.Id !== id);
    // setLocalStorage(listBlog);
    // alert("Deleting successful.");
    // readBlog();

    // _blogId = "";
    // $("#title").val("");
    // $("#author").val("");
    // $("#content").val("");

    Notiflix.Confirm.show(
        'Confirm',
        'Are you sure want to delete?',
        'Yes',
        'No',
        function okCb() {
            Notiflix.Block.dots('#form1');

            setTimeout(() => {
                let listBlog = getBlogs();
                let itemList = listBlog.filter((x) => x.Id === id);
                if (itemList.length === 0) {
                    console.log("No data found");
                    return;
                }
                listBlog = listBlog.filter((x) => x.Id !== id);
                setLocalStorage(listBlog);

                Notiflix.Block.remove('#form1');

                successMessage("Deleting successful.");
                readBlog();

                _blogId = "";
                $("#title").val("");
                $("#author").val("");
                $("#content").val("");
            }, 2000);
        },
        function cancelCb() {

        },
    );
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
        (
            c ^
            (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
        ).toString(16)
    );
}

function getBlogs() {
    let listBlogs = [];
    let blogStr = localStorage.getItem(tblBlogs);
    if (blogStr != null) {
        listBlogs = JSON.parse(blogStr);
    }
    return listBlogs;
}

function setLocalStorage(lst) {
    let jsonStr = JSON.stringify(lst);
    localStorage.setItem(tblBlogs, jsonStr);
}

$("#btnSave").click(function () {
    const title = $("#title").val();
    const author = $("#author").val();
    const content = $("#content").val();

    if (_blogId === "") {
        Notiflix.Loading.circle();
        setTimeout(() => {
            createBlog(title, author, content);
            Notiflix.Loading.remove();
            successMessage("Saving successful.");
        }, 2000);
    } else {
        Notiflix.Loading.circle();
        setTimeout(() => {
            updateBlog(_blogId, title, author, content);
            Notiflix.Loading.remove();
            successMessage("Updating successful.");
            _blogId = "";
        }, 2000);
    }

    $("#title").val("");
    $("#author").val("");
    $("#content").val("");

    $("#title").focus();

    readBlog();
});

function successMessage(message) {
    // Notiflix.Notify.success(message);

    Notiflix.Report.success(
        'Success',
        message,
        'Okay',
    );
}
