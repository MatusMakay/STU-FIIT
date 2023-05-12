<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class shopping_cart extends Model
{
    use HasFactory;
    // creating of deleted att in the table so we can have the history of deleted events
    use SoftDeletes;
    protected $dates = ['deleted_at'];

    // creating the relation between the tables in the database
    public function order(){
        return $this->hasMany('App\order');
    }

    public function user(){
        return $this->belongsTo(user::class, 'user_id');
    }
}
