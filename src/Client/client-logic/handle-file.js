function handleFile(file){
    var form  = new FormData();
    form.append('file', file);
    var promise = fetch('/handle',
    {
        method:'POST',
        body:form
    })
    .then(response => response.json());
    return promise;
}

export default handleFile;
