$(document).ready(function () {
  $('#myModal').on('show.bs.modal', function (e) {
    var image = $(e.relatedTarget).attr('src');
    $(".img-responsive").attr("src", image);
  });
})
