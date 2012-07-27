$(document).ready(function () {

    $("#addNewProjectLink").click(function (evt) {

        evt.preventDefault();
        var project = getProject();
        if (project == null) {
            $("#projectCreationOutcome").text = "You didn't supply a name for the project.";
        } else {
            $.post("/TaskBoard/CreateProject", getProject(), function (result) {

            });
        }

    });

    $('.projectAdminLinks a').hover(
        function () {
            var pos = $(this).position();

            $('#editProjectToolTip').css({
                top: pos.top + 28 + "px",
                left: pos.left - 50 + "px"
            }
            ).slideDown(500);
        },
        function () {
            $('#editProjectToolTip').slideUp(500, "linear");
        });

    $('img[id^="showDetails_"]').hover(function () {
        var pos = $(this).position();
        $('#ShowDescriptionToolTip').css({
            top: pos.top + 28 + "px",
            left: pos.left - 50 + "px"
        }
            ).slideDown(500);
    },
        function () {
            $('#ShowDescriptionToolTip').slideUp(500);
        });

    $('img[id^="showDetails_"]').click(function (evt) {
        var detailsContainerToShow = evt.target.id.replace("showDetails_", "DetailsContainer_");
        $('#' + detailsContainerToShow).slideDown(1000);
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