<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateAddressesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('addresses', function (Blueprint $table) {
            $table->id();
            $table->timestamps();
            $table->string('address')->nullable(false);
            //FK
            $table->unsignedbigInteger('general_page_id');
            $table->foreign('general_page_id')->references('id')->on('general_pages');
        });
        
        //Проверка на валидный адрес
        DB::statement("ALTER TABLE addresses ADD CONSTRAINT valid_address CHECK (trim(address) <> '' AND address ~* '^[А-Яа-я0-9.,-<> №() ]{7,}$')");
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('addresses');
    }
}
