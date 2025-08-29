// Initialize the Select2 component
//import { createPopper } from '@popperjs/core';

$('#departments2').select2();
//$('departments6').

//$('#PageSize').select2();
function setParamsInForm() {
    const url = new URL(window.location.href);
    if (!url) return; // No referrer available

    //const url = new URL(url);
    const params = url.searchParams;
    var searchTerm = params.get("SearchTerm");
    document.getElementById("searchInput").value = searchTerm;

    var ageMin = params.get("AgeMin");
    var ageMax = params.get("AgeMax");

    if (ageMin && ageMax) {
        document.getElementById("AgeMin").value = ageMin; // Set hidden input
        document.getElementById("ageMin").textContent = ageMin; // Set displayed min age
        document.getElementById("AgeMax").value = ageMax; // Set hidden input
        document.getElementById("ageMax").textContent = ageMax; // Set displayed max age
        // Update the slider
        //$('#ageRangeSlider').slider("setValue", [parseInt(ageMin), parseInt(ageMax)]);-- this was the line which  gave me error 
    }

    // Retrieve and set the SalaryMin and SalaryMax (if they exist)
    var salaryMin = params.get("SalaryMin");
    var salaryMax = params.get("SalaryMax");
    if (salaryMin && salaryMax) {
        document.getElementById("SalaryMin").value = salaryMin; // Set hidden input
        document.getElementById("salaryMin").textContent = salaryMin; // Set displayed min salary
        document.getElementById("SalaryMax").value = salaryMax; // Set hidden input
        document.getElementById("salaryMax").textContent = salaryMax;
        // Set displayed max salary
        // Update the slider
    }
    var sortby = params.get("SortBy");
    var sortorder = params.get("SortOrder");
    if (sortby) {
        if (sortby == "studentage" || sortby == "studentsalary") {

            document.getElementById(sortby).classList.add(sortorder == "ASC" ? "bi-sort-numeric-down-alt" : "bi-sort-numeric-up")


        } else {

            document.getElementById(sortby).classList.add(sortorder == "ASC" ? "bi-sort-up" : "bi-sort-down")
        }
    } else
    {


        document.getElementById("FullName").classList.add( "bi-sort-up" )


    }
    document.querySelectorAll('.sortable-header').forEach(function (otherHeader) {
        if (otherHeader.querySelector('i').id !== sortby) {
            if (otherHeader.querySelector('i').id == "studentage" || otherHeader.querySelector('i').id == "studentsalary") {
                const otherIcon = otherHeader.querySelector('i');
                otherIcon.classList.add("bi-sort-numeric-down-alt")  // This will remove rotation from other icons
            } else {
                const otherIcon = otherHeader.querySelector('i');
                otherIcon.classList.add("bi-sort-down")  // This will remove rotation from other icons
            }



        }
                });
    var depart = params.getAll("DeptIds");
    $('#departments2').val(depart);
    $('#departments2').trigger('change');// it falicitates the multiple select visbility functionality !!!
}

// Call the function to set form values
setParamsInForm();

// Function to fetch data from an endpoint with a JWT token
function fetchData(url) {
    const token = localStorage.getItem('jwtToken');  // Retrieve the token from localStorage

    if (!token) {
        console.error('No token found');
        return;
    }

    // Fetch data from the provided URL
    fetch(url, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`,  // Attach the token as a Bearer token
            'Content-Type': 'application/json'    // Optional, depending on your API needs
        }
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Unauthorized or Bad Request');
            }
        })
        .then(data => console.log('Data received:', data))
        .catch(error => console.error('Error:', error));
}

////  usage of the function
//fetchData('/Home/Index');
//fetchData('/api/protected');

//// Function to save the token to localStorage and handle cookies
//function saveToken(token) {
//    if (token) {
//        localStorage.setItem('jwtToken', token);  // Store token in localStorage

//        // Optional: Store the token in a cookie for additional security
//        document.cookie = `token=${token}; path=/; Secure; SameSite=Strict`;  // Store in a cookie
//    }
//}

//// saving the token (this would be done after login)
const response = { token: 'your_jwt_token_here' };
// Assume this comes from the login response
//saveToken(response.token);

//// Optionally, you can clear the token if the user logs out
//function clearToken() {
//    localStorage.removeItem('jwtToken');  // Remove the token from localStorage
//    document.cookie = 'token=; path=/; expires=Thu, 01 Jan 1970 00:00:00 GMT';  // Clear the cookie
//}

//// Example of clearing the token (e.g., when logging out)
//clearToken();
// JavaScript for toggling the icon rotation

//document.querySelectorAll('.sortable-header').forEach(function (header) {
//    header.addEventListener('click', function () {
//        console.log('Header clicked');  // This should print when you click any header
//        const icon = header.querySelector('i');
//        icon.classList.toggle('icon-rotate');

//        document.querySelectorAll('.sortable-header').forEach(function (otherHeader) {
//            if (otherHeader !== header) {
//                const otherIcon = otherHeader.querySelector('i');
//                otherIcon.classList.remove('icon-rotate');
//            }
//        });
//    });
//});

//document.addEventListener('DOMContentLoaded', function () {
//    document.querySelectorAll('.sortable-header').forEach(function (header) {
//        header.addEventListener('click', function () {
//            const icon = header.querySelector('i');
//            icon.classList.remove('bi-sort-down');
//            icon.classList.add('bi-sort-up');
//            // This will toggle the rotation of the icon

//            document.querySelectorAll('.sortable-header').forEach(function (otherHeader) {
//                if (otherHeader !== header) {
//                    const otherIcon = otherHeader.querySelector('i');
//                    otherIcon.classList.remove('icon-rotate');  // This will remove rotation from other icons
//                }
//            });
//        });
//    });
//});