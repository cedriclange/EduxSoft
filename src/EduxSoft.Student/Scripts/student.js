(function ($) {
    function Student() {
        var $this = this;
        function InitializeModel() {
            $("#modal-action-classe").on('loaded.bs.modal', function (e) {

            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });
        }
        $this.init = function () { InitializeModel() }

    }
    $(function () {
        var self = new Student();
        self.init();
    })
}(jQuery))