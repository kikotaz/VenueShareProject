//This is initialization of the label below upload file control
var imagesAlert = document.getElementById('venueImagesUploaded');

//This is initialization of the label below description
var label = document.getElementById('descriptionRemaining');

//This is initialization of the label of CustomerID
var customerID = document.getElementById('CustomerIDResultLabel');

//This function will show the initial character count for the description filed at page load
window.onload = function () {

    label.innerHTML = 300;


    //Initial value of the images Label
    imagesAlert.innerHTML = "Maximum 5 images";
};

//This function will count the remaining characters every time the user types a character
function CharCount(textString) {
    //This will get the characters count in the string
    var count = textString.value.length;

    //This if statement will stop typing if count is equal to 300 characters
    if (count > 300) {
        textString.value = textString.value.substring(0, 300);
        var remaining = 300 - count + 1;
    }
    //This else will count the remaining as long as less than 300
    else {
        var remaining = 300 - count;
    }
    label.innerHTML = remaining.toString();
}

//This function will change the label when files are uploaded
$('input#venueImagesUpload').change(function () {

    //Creating list of files inside
    var images = $(this)[0].files;
    //Mapping the content to get their names
    var names = $.map(images, function (img) {
        return ' ' + img.name;
    });


    //Show error if more than 5 images are uploaded
    if (images.length > 5) {
        imagesAlert.innerHTML = "Can't upload more than five images";
    }
    else {
        //Setting the label to show te images names
        imagesAlert.innerHTML = 'Uploaded images ' + names;
    }
});


function callServer(boxContent) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "/Admin/AdminAddVenue.aspx/GetCustomer",
        crossDomain: true,
        data: "{ 'email' :'" + boxContent.value + "'}",
        dataType: "json",
        success: function (result) {

            if (result.d !== null) {

                customerID.innerHTML = result.d.CustomerID;
            }
            else {
                alert('This user does not exist.');
            }
        },
        error: function () {
            alert('Something went wrong');
        }
    });
}