function onRecordClick(data) {
    console.log("123");
    var myModal = new bootstrap.Modal(document.getElementById('modalCenter'))
    var client = JSON.parse(data);
    for (var prop in client) {
        console.log(prop)
        const inputText = document.getElementById(prop);
        if (inputText != null) {
            inputText.value = client[prop]
            console.log(prop);
        }
    }
    myModal.show();
}

function onRecordClickNew() {
    var myModal = new bootstrap.Modal(document.getElementById('modalCenter'))
    myModal.show();
}

function ShowModal(modalId) {
    var myModal = new bootstrap.Modal(document.getElementById(modalId))
    myModal.show();
}