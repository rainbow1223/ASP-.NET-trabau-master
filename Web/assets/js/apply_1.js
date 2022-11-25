function LoadApplyJobEvents() {
    $(document).ready(function () {
        $('.file-upload').click(function () {
            $('input[id*="afuProjectFiles"]').click();
        });

        $.event.special.inputchange = {
            setup: function () {
                var self = this, val;
                $.data(this, 'timer', window.setInterval(function () {
                    val = self.value;
                    if ($.data(self, 'cache') != val) {
                        $.data(self, 'cache', val);
                        $(self).trigger('inputchange');
                    }
                }, 20));
            },
            teardown: function () {
                window.clearInterval($.data(this, 'timer'));
            },
            add: function () {
                $.data(this, 'cache', this.value);
            }
        };

        $('input[id*="txtBid_FixedPrice"]').focusin(function () {
            RegisterFP_BidChangeEvent();
        });

        $('input[id*="txtReceive_FixedPrice"]').focusin(function () {
            RegisterFP_ReceiveChangeEvent();
        });


        $('input[id*="txtBid_HourlyRate"]').focusin(function () {
            RegisterHR_BidChangeEvent();
        });

        $('input[id*="txtReceive_HourlyRate"]').focusin(function () {
            RegisterHR_ReceiveChangeEvent();
        });


    });
}