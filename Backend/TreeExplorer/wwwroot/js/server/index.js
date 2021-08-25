function set() {
    fetch('https://localhost:5001/Elements/Set')
        .then(response => response.json())
        .then(data => {
            console.log(data)
            if (data.status === 200) {
                document.getElementById("responde").innerText = "Data is set";
                document.getElementById("set_button").disabled = true;
            }
            else {
                document.getElementById("responde").innerText = "Server not responde";
            }
        });
}