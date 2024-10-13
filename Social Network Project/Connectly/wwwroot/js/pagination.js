document.addEventListener("DOMContentLoaded", function () {
    var totalPages = parseInt(document.getElementById("totalPages").value, 10);
    var currentPage = parseInt(document.getElementById("currentPage").value, 10);
    var pageRange = 1;
    function updatePagination() {
        document.querySelectorAll(".pagination .page-item").forEach(item => {
            item.style.display = "none";
        });

        document.getElementById("firstPage").parentNode.style.display = "inline-block";
        document.getElementById("previousPage").parentNode.style.display = "inline-block";
        document.getElementById("nextPage").parentNode.style.display = "inline-block";
        document.getElementById("lastPage").parentNode.style.display = "inline-block";

        document.querySelector('[data-page="1"]').style.display = "inline-block";
        document.querySelector('[data-page="' + totalPages + '"]').style.display = "inline-block";

        for (var i = Math.max(2, currentPage - pageRange); i <= Math.min(totalPages - 1, currentPage + pageRange); i++) {
            document.querySelector('[data-page="' + i + '"]').style.display = "inline-block";
        }

        if (currentPage > pageRange + 2) {
            var ellipsis = document.createElement("li");
            ellipsis.className = "page-item disabled";
            ellipsis.innerHTML = '<span class="page-link">...</span>';
            document.querySelector('[data-page="1"]').after(ellipsis);
        }

        if (currentPage < totalPages - pageRange - 1) {
            var ellipsis = document.createElement("li");
            ellipsis.className = "page-item disabled";
            ellipsis.innerHTML = '<span class="page-link">...</span>';
            document.querySelector('[data-page="' + (totalPages - 1) + '"]').before(ellipsis);
        }

        document.getElementById("previousPage").setAttribute("asp-route-pageNumber", currentPage - 1);
        document.getElementById("nextPage").setAttribute("asp-route-pageNumber", currentPage + 1);

        if (currentPage === 1) {
            document.getElementById("previousPage").classList.add("pointer-events-none");
        } else {
            document.getElementById("previousPage").classList.remove("pointer-events-none");
        }

        if (currentPage === totalPages) {
            document.getElementById("nextPage").classList.add("pointer-events-none");
        } else {
            document.getElementById("nextPage").classList.remove("pointer-events-none");
        }
    }

    updatePagination();
});