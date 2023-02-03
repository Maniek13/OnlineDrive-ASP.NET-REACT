function set() {
    fetch('http://178.235.60.107:5002/Elements/Set')
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