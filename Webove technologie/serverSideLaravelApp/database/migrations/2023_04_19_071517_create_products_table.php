<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('products', function (Blueprint $table) {
            $table->id();
            $table->string('item_name', 400);
            $table->string('product_img', 255);
            $table->string('description', 1024);
            $table->double('product_price', 8, 2);
            $table->enum('category',['Man','Women','Kids']);
            $table->enum('type',['LongSleeve','ShortSleeve','Hoddies', 'Jeans', 'ShortJeans']);
            $table->enum('brand',['Adidas','Nike','Supreme', 'Humanic']);
            $table->enum('color',['White','Black','Pink']);
            $table->enum('size',['S','M','L','XL','XXL']);
            $table->integer('number_products');
            $table->timestamps();
            $table->timestamp('deleted_at')->nullable();

        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('products');
    }
};
