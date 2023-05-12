<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class order extends Model
{
    use HasFactory;
    // creating of deleted att in the table so we can have the history of deleted events
    use SoftDeletes;
    protected $dates = ['deleted_at'];
    protected $casts = [
        'productsIDs' => 'array'
    ];
    // creating the relation between the tables in the database
    public function payment(){
        return $this->hasMany('app\payment');
    }

    public function delivery(){
        return $this->hasMany('app\delivery');
    }

}
