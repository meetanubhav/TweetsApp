var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#myDT').DataTable({
        "ajax": {
            "url": "/Account/GetAllTweets",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            // { "data": "Id"},
            // { "data": "UserId"},
            { "data": "username"},
            { "data": "tweets"}
        ],
        "language": {
            "emptyTable": "no data found"
        }
    });
}