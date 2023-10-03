# JSwitch API Reference

```
Operate() - This is how you actually use a JSwitch. By calling Operate, the JSwitch will analyze all of its branches and see if it can find ones that have valid conditions. These are ranked by their priority, and the branch with the highest priority has OnDemand() called on its JTrigger. If none of the branches are valid, it can run OnDemand() on a default event if one is assigned.
```
