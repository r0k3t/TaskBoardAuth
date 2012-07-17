$(document).ready(function () {

    $("addNewProjectLink").click(function (evt) {
        evt.preventDefault();
        var project = getProject();
        if (project == null) {
            $("#projectCreationOutcome").text = "You didn't supply a name for the project.";
        } else {
            $.post("/TaskBoard/CreateProject", getProject(), function (result) {
                
            });
        }

    });

    function getProject() {
        var name = $("#projectName").val();
        var description = $("#projectDescription").val();
        return (name == "") ? null : {
            Name: name,
            Description: description
        };
    }
});

