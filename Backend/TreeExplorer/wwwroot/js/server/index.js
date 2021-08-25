function set() {
    fetch('https://localhost:5001/Elements/Set')
        .then(response => response.json())
        .then(data => {
            if (data.ok === true) {
                document.getElementById("responde").innerText = "Data is set";
                document.getElementById("set_button").disabled = true;
            }
            else {
                document.getElementById("responde").innerText = "Server not responde";
            }
        });
}