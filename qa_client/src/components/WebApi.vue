<template>
    
</template>

<script>
    export default {
        name: "WebApi",
        data: new function() {
            authToken: {}
        },
        methods: {
            loadQuestions: function (onLoaded) {
                fetch('/api/questions')
                .then(result, result.json().then)
            },
            doRequest(url, onError, onSuccess) {
                fetch(url)
                    .then(response => {
                        if (response.ok) {
                            //result.json().then()
                        } else {
                            onError("HttpError: " + response.status)
                        }
                    })
                
            }
        }
    }
</script>