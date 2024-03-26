package com.redhat.cloudnative;

import io.quarkus.hibernate.orm.panache.PanacheEntity;
import jakarta.persistence.Entity;
import jakarta.persistence.Table;

@Entity
@Table(name="INVENTORY")
public class Inventory extends PanacheEntity {
    public int quantity;
}
