
public class Health 
{
    int health;
   
   public Health(int health){
       this.health=health;
   } 
    public int GetHealth(){
        return health;
    }
    public void Damage(int dmg){
        health-=dmg;
    }
    public void Heal(int heal){
        health += heal;
    }
   
}
