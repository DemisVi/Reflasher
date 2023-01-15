using System;
using System.Management;

namespace Reflasher;

public class WqlQueries
{
    public static readonly WqlEventQuery creationEventQuery = new WqlEventQuery(
        "SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE " +
        "TargetInstance ISA 'Win32_PnPEntity' " +
        "AND TargetInstance.Caption LIKE '%Telit Serial Diagnostics Interface%'");

    public static readonly WqlEventQuery creationA73Query = new(
        "SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE " +
        "TargetInstance ISA 'Win32_PnPEntity' " +
        "AND TargetInstance.ClassGuid = '{eec5ad98-8080-425f-922a-dabf3de3f69a}'");

    public static readonly WqlEventQuery creationTsQuery = new(
        "SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE " +
        "TargetInstance ISA 'Win32_PnPEntity' " +
        "AND TargetInstance.ClassGuid = '{4d36e967-e325-11ce-bfc1-08002be10318}'");

    public static readonly WqlEventQuery deletionEventQuery = new WqlEventQuery(
        "SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE " +
        "TargetInstance ISA 'Win32_PnPEntity' " +
        "AND TargetInstance.Caption LIKE '%Telit Serial Diagnostics Interface%'");

    public static readonly WqlEventQuery deletionA73Query = new(
        "SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE " +
        "TargetInstance ISA 'Win32_PnPEntity' " +
        "AND TargetInstance.ClassGuid = '{eec5ad98-8080-425f-922a-dabf3de3f69a}'");

    public static readonly WqlEventQuery deletionTsQuery = new(
        "SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE " +
        "TargetInstance ISA 'Win32_PnPEntity' " +
        "AND TargetInstance.ClassGuid = '{4d36e967-e325-11ce-bfc1-08002be10318}'");

    public static readonly WqlObjectQuery deviceQuery = new WqlObjectQuery(
        "SELECT * FROM Win32_PnPEntity WHERE " +
        "Caption LIKE '%Telit Serial Diagnostics Interface%'");

    public static readonly WqlObjectQuery a73Query = new WqlObjectQuery(
        "SELECT * FROM Win32_PnPEntity WHERE ClassGuid = '{eec5ad98-8080-425f-922a-dabf3de3f69a}'");

    public static readonly WqlObjectQuery tsQuery = new WqlObjectQuery(
        "SELECT * FROM Win32_PnPEntity WHERE ClassGuid = '{4d36e967-e325-11ce-bfc1-08002be10318}'");
}