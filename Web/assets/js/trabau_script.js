function Validator_Events() {
    $(document).ready(function () {
      //  AutoValidatorsRemove();

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

        //$('input[type="text"]').on('inputchange', function () {
        //    CheckValidators();
        //});

        //$('input[type="password"]').on('inputchange', function () {
        //    $(this).parent('div').removeClass('has-error');
        //});

        $('input[type="button"]').click(function () {
            CheckValidators();
        });

        $('input[type="submit"]').click(function () {
            CheckValidators();
        });

        $('select').change(function () {
            CheckValidators();
        });
    });
}

function CheckValidators() {
    $('span[class="text-danger"]').parent('div').removeClass('has-error');
    $('span[class="text-danger"][style*="inline"]').parent('div').addClass('has-error');
}

function AutoValidatorsRemove() {
    $('span[class="text-danger"][style*="none"]').parent('div').removeClass('has-error');

    setTimeout(function () {
        AutoValidatorsRemove();
    }, 500);
}




